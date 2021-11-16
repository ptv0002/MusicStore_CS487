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
    public class EmployeeController : Controller
    {
        private readonly MusicStore_Context _context;
        public EmployeeController(MusicStore_Context context)
        {
            _context = context;
        }
        // GET: EmployeeController
        public ActionResult Index()
        {
            return View(_context.Employees.ToList());
        }
        // GET: EmployeeController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var model = await _context.Employees.FindAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            return View(new Employee());
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Employee employee)
        {
            try
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _context.Employees.FindAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Employee employee)
        {
            try
            {
                var item = _context.Employees.Find(employee.ID);
                item.FirstName = employee.FirstName;
                item.LastName = employee.LastName;
                item.Address = employee.Address;
                item.Phone = employee.Phone;
                item.Position = employee.Position;
                item.Salary = employee.Salary;
                _context.Employees.Update(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(employee);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var item = await _context.Employees.FindAsync(id);
                if (item == null) return NotFound();
                else
                {
                    _context.Employees.Remove(item);
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
