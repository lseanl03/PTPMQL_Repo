using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoMVC.Data;
using DemoMVC.Models.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Demo_MVC.Controllers
{
    [Authorize]
    public class Employee1Controller : Controller
    {
        private readonly ApplicationDbContext _context;

        public Employee1Controller(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Employee1.ToListAsync());
        }

        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee1 = await _context.Employee1
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee1 == null)
            {
                return NotFound();
            }

            return View(employee1);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,FirstName,LastName,Address,DateOfBirth,Position,Email,HireDate")] Employee1 employee1)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee1);
        }
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee1 = await _context.Employee1.FindAsync(id);
            if (employee1 == null)
            {
                return NotFound();
            }
            return View(employee1);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,FirstName,LastName,Address,DateOfBirth,Position,Email,HireDate")] Employee1 employee1)
        {
            if (id != employee1.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Employee1Exists(employee1.EmployeeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee1);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee1 = await _context.Employee1
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee1 == null)
            {
                return NotFound();
            }

            return View(employee1);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee1 = await _context.Employee1.FindAsync(id);
            if (employee1 != null)
            {
                _context.Employee1.Remove(employee1);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Employee1Exists(int id)
        {
            return _context.Employee1.Any(e => e.EmployeeId == id);
        }
    }
}
