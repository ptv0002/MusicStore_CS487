using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly MusicStore_Context _context;
        public CustomerController(MusicStore_Context context)
        {
            _context = context;
        }
        // GET: CustomerController
        public ActionResult Index()
        {
            return View(_context.Customers.ToList());
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View(new Customer());
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: CustomerController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _context.Customers.FindAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Customer customer)
        {
            try
            {
                var item = _context.Customers.Find(customer.ID);
                item.FirstName = customer.FirstName;
                item.LastName = customer.LastName;
                item.Address = customer.Address;
                item.Phone = customer.Phone;
                _context.Update(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(customer);
            }
        }

        // GET: CustomerController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var model = await _context.Customers.FindAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        // POST: CustomerController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var item = await _context.Customers.FindAsync(id);
                if (item == null) return NotFound();
                else
                {
                    _context.Customers.Remove(item);
                    var result = await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return RedirectToAction("Details", new { id });
            }
        }
    }
}
