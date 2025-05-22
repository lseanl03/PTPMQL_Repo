using DemoMVC.Data;
using DemoMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using VicemMVIdentity.Models.Process;

namespace DemoMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly EmployeeSeeder _seeder;

        public AdminController(ApplicationDbContext context, EmployeeSeeder seeder)
        {
            _context = context;
            _seeder = seeder;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: Admin/ResetEmployees
        public IActionResult ResetEmployees()
        {
            return View();
        }

        // POST: Admin/ResetEmployees
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetEmployees(bool confirm)
        {
            if (confirm)
            {
                // Xóa tất cả nhân viên hiện có
                _context.Employee1.RemoveRange(_context.Employee1);
                await _context.SaveChangesAsync();

                // Tạo 10 nhân viên mới
                _seeder.SeedEmployees(10);

                TempData["Message"] = "Danh sách nhân viên đã được thiết lập lại với 10 nhân viên.";
            }

            return RedirectToAction("Index", "Employee1");
        }
    }
}
