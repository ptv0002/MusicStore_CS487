using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using Music_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly MusicStore_Context _context;
        public OrderController(MusicStore_Context context)
        {
            _context = context;
        }
        // GET: OrderController
        public ActionResult Index()
        {
            var list = _context.OrderInfos.Include(m => m.Customer).Include(m => m.Employee).ToList();
            List<OrderViewModel> model = new();
            foreach (var item in list)
            {
                var sum = _context.OrderDetails.Where(m => m.OrderInfoID == item.ID).Sum(m => m.QuantitySold * m.UnitPrice);
                var order = new OrderViewModel()
                {
                    ID = item.ID,
                    DateOfOrder = item.DateOfOrder,
                    TotalOrder = Math.Round((decimal)sum,2),
                    EmployeeID = item.Employee.ID,
                    EFirstName = item.Employee.FirstName,
                    ELastName = item.Employee.LastName,
                    CustomerID = item.Customer.ID,
                    CFirstName = item.Customer.FirstName,
                    CLastName = item.Customer.LastName,
                    CPhone = item.Customer.Phone
                };
                model.Add(order);
            }
            return View(model);
        }

        // GET: OrderController/Details/5
        public async Task<IActionResult> Details(int id)
        {

            return await ViewGenerator(id);
        }
        public async Task<IActionResult> ViewGenerator(int id)
        {
            var order = await _context.OrderInfos.FindAsync(id);
            var employee = await _context.Employees.FindAsync(order.EmployeeID);
            var customer = await _context.Customers.FindAsync(order.CustomerID);
            List<OrderDetailViewModel> details = new();
            foreach (OrderDetail item in _context.OrderDetails.Where(m => m.OrderInfoID == id))
            {
                var instrumentInfo = _context.Instruments.Find(item.InstrumentID);
                OrderDetailViewModel detailItem = new()
                {
                    InstrumentID = item.InstrumentID,
                    Brand = instrumentInfo.Brand,
                    Model = instrumentInfo.Model,
                    Type = instrumentInfo.InstrumentType,
                    UnitPrice = item.UnitPrice,
                    QuantitySold = item.QuantitySold,
                    TotalPrice = Math.Round((decimal)(item.UnitPrice * item.QuantitySold),2)
                };
                details.Add(detailItem);
            }
            var sum = details.Sum(m => m.TotalPrice);
            var model = new OrderViewModel
            {
                ID = id,
                DateOfOrder = order.DateOfOrder,
                TotalOrder = Math.Round((decimal)sum,2),
                EmployeeID = employee.ID,
                EFirstName = employee.FirstName,
                ELastName = employee.LastName,
                CustomerID = customer.ID,
                CFirstName = customer.FirstName,
                CLastName = customer.LastName,
                CPhone = customer.Phone,
                OrderDetails = details
            };
            return View(model);
        }
        // GET: OrderController/Create
        public ActionResult Create()
        {
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "ID", "ID");
            ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "ID");
            return View(new OrderInfo());
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderInfo orderInfo)
        {
            try
            {
                _context.Add(orderInfo);
                await _context.SaveChangesAsync();

                // Get the Id of newest item
                var newItem = await _context.OrderInfos.OrderBy(m => m.ID).LastAsync();
                return RedirectToAction(nameof(Edit), new { id = newItem.ID });
            }
            catch
            {
                ViewData["EmployeeID"] = new SelectList(_context.Employees, "ID", "ID", orderInfo.EmployeeID.ToString());
                ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "ID", orderInfo.CustomerID.ToString());

                return View(orderInfo);
            }
        }

        // GET: OrderController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var orderInfo = await _context.OrderInfos.FindAsync(id);
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "ID", "ID", orderInfo.EmployeeID.ToString());
            ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "ID", orderInfo.CustomerID.ToString());
            return await ViewGenerator(id);
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OrderViewModel orderInfo)
        {
            try
            {
                var item = await _context.OrderInfos.FindAsync(id);
                item.CustomerID = orderInfo.CustomerID;
                item.EmployeeID = orderInfo.EmployeeID;
                item.DateOfOrder = orderInfo.DateOfOrder;
                _context.OrderInfos.Update(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["EmployeeID"] = new SelectList(_context.Employees, "ID", "ID", orderInfo.EmployeeID.ToString());
                ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "ID", orderInfo.CustomerID.ToString());
                return View(orderInfo);
            }
        }

        // GET: OrderController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var orderInfo = await _context.OrderInfos.FindAsync(id);
                _context.Remove(orderInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Details), new { id });
            }
        }
    }
}
