using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_Web.Controllers
{
    public class OrderDetailController : Controller
    {
        private readonly MusicStore_Context _context;
        public OrderDetailController(MusicStore_Context context)
        {
            _context = context;
        }
        // GET: OrderDetailController/Create
        public ActionResult Create(int id)
        {
            ViewData["InstrumentID"] = new SelectList(_context.Instruments, "ID", "ID");
            return View(new OrderDetail() { OrderInfoID = id});
        }

        // POST: OrderDetailController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderDetail orderDetail)
        {
            try
            {
                var item = new OrderDetail
                {
                    ID = null,
                    OrderInfoID = orderDetail.OrderInfoID,
                    InstrumentID = orderDetail.InstrumentID,
                    QuantitySold = orderDetail.QuantitySold,
                    UnitPrice = orderDetail.UnitPrice
                };

                _context.Add(item);
                var instrument = await _context.Instruments.FindAsync(orderDetail.InstrumentID);
                instrument.Quantity -= orderDetail.QuantitySold;
                if (instrument.Quantity < 0)
                {
                    ModelState.AddModelError(string.Empty,"Not enough instrument in stock");
                    ViewData["InstrumentID"] = new SelectList(_context.Instruments, "ID", "ID", orderDetail.InstrumentID.ToString());
                    return View(orderDetail);
                }
                _context.Update(instrument);
                await _context.SaveChangesAsync();
                return RedirectToAction("Edit", "Order", new { id = orderDetail.OrderInfoID });
            }
            catch
            {
                ViewData["InstrumentID"] = new SelectList(_context.Instruments, "ID", "ID", orderDetail.InstrumentID.ToString());
                return View(orderDetail);
            }
        }
        public async Task<IActionResult> Details(int id)
        {
            var item = await _context.OrderDetails.FindAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }
        // GET: OrderDetailController/Edit/5
        public ActionResult Edit(int id)
        {
            var item = _context.OrderDetails.Find(id);
            ViewData["InstrumentID"] = new SelectList(_context.Instruments, "ID", "ID",item.InstrumentID.ToString());
            return View(item);
        }

        // POST: OrderDetailController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OrderDetail orderDetail)
        {
            try
            {
                var item = await _context.OrderDetails.FindAsync(id);
                var instrument = await _context.Instruments.FindAsync(orderDetail.InstrumentID);
                instrument.Quantity = instrument.Quantity + item.QuantitySold - orderDetail.QuantitySold;
                if (instrument.Quantity < 0)
                {
                    ModelState.AddModelError(string.Empty, "Not enough instrument in stock");
                    ViewData["InstrumentID"] = new SelectList(_context.Instruments, "ID", "ID", orderDetail.InstrumentID.ToString());
                    return View(orderDetail);
                }
                _context.Update(instrument);
                
                item.InstrumentID = orderDetail.InstrumentID;
                item.QuantitySold = orderDetail.QuantitySold;
                item.UnitPrice = orderDetail.UnitPrice;
                _context.Update(item);

                
                await _context.SaveChangesAsync();
                return RedirectToAction("Edit", "Order", new { id = orderDetail.OrderInfoID});
            }
            catch
            {
                ViewData["InstrumentID"] = new SelectList(_context.Instruments, "ID", "ID",orderDetail.InstrumentID.ToString());
                return View(orderDetail);
            }
        }

        // GET: OrderDetailController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }
    }
}
