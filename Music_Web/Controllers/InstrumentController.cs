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
    public class InstrumentController : Controller
    {
        private readonly MusicStore_Context _context;
        public InstrumentController(MusicStore_Context context)
        {
            _context = context;
        }
        // GET: InstrumentController
        public ActionResult Index()
        {
            return View(_context.Instruments.ToList());
        }
        // GET: InstrumentController/Details
        public async Task<IActionResult> Details(int id)
        {
            var model = await _context.Instruments.FindAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }
        // GET: InstrumentController/Create
        public ActionResult Create()
        {
            return View(new Instrument());
        }

        // POST: InstrumentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Instrument instrument)
        {
            if (ModelState.IsValid)
            {
                _context.Add(instrument);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(instrument);
        }

        // GET: InstrumentController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _context.Instruments.FindAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        // POST: InstrumentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Instrument instrument)
        {
            try
            {
                var item = _context.Instruments.Find(instrument.ID);
                item.Brand = instrument.Brand;
                item.InstrumentType = instrument.InstrumentType;
                item.Model = instrument.Model;
                item.UnitPrice = instrument.UnitPrice;
                item.Quantity = instrument.Quantity;
                _context.Update(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(instrument);
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var item = await _context.Instruments.FindAsync(id);
                if (item == null) return NotFound();
                else
                {
                    _context.Instruments.Remove(item);
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
