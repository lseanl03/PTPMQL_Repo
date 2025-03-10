using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoMVC.Data;
using Demo_MVC.Models;

namespace Demo_MVC.Controllers
{
    public class DailyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DailyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Daily
        public async Task<IActionResult> Index()
        {
            return View(await _context.Daily.ToListAsync());
        }

        // GET: Daily/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var daily = await _context.Daily
                .FirstOrDefaultAsync(m => m.Id == id);
            if (daily == null)
            {
                return NotFound();
            }

            return View(daily);
        }

        // GET: Daily/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Daily/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaDaiLy,TenDaiLy,NguoiDaiDien,DienThoai,Id,MaHTPP,TenHTPP")] Daily daily)
        {
            if (ModelState.IsValid)
            {
                _context.Add(daily);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(daily);
        }

        // GET: Daily/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var daily = await _context.Daily.FindAsync(id);
            if (daily == null)
            {
                return NotFound();
            }
            return View(daily);
        }

        // POST: Daily/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaDaiLy,TenDaiLy,NguoiDaiDien,DienThoai,Id,MaHTPP,TenHTPP")] Daily daily)
        {
            if (id != daily.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(daily);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DailyExists(daily.Id))
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
            return View(daily);
        }

        // GET: Daily/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var daily = await _context.Daily
                .FirstOrDefaultAsync(m => m.Id == id);
            if (daily == null)
            {
                return NotFound();
            }

            return View(daily);
        }

        // POST: Daily/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var daily = await _context.Daily.FindAsync(id);
            if (daily != null)
            {
                _context.Daily.Remove(daily);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DailyExists(int id)
        {
            return _context.Daily.Any(e => e.Id == id);
        }
    }
}
