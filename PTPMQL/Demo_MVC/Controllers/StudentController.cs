using Demo_MVC.Models;
using DemoMVC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoMVC.Controllers
{
    public class StudentController : Controller
    {
        //khai bao dbcontext
        private readonly ApplicationDbContext _context;
        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var students = await _context.student.ToListAsync();
            return View(students);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id, FullName, Email")] Student student)
        {
            await _context.student.AddAsync(student);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0 || _context.student == null) return NotFound();

            var student = await _context.student.FindAsync(id);

            if (student == null) return NotFound();

            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, FullName, Email")] Student student)
        {
            if (id == 0 || !ModelState.IsValid) return NotFound();

            _context.Update(student);
            
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0 || _context.student == null) return NotFound();

            var student = await _context.student.FindAsync(id);

            if (student == null) return NotFound();

            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.student == null) return Problem("Null students");

            var student = await _context.student.FindAsync(id);

            if (student != null) _context.student.Remove(student);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        private bool PersonExists(int id)
        {
            return (_context.student?.Any(p => p.Id == id)).GetValueOrDefault();
        }    
        public IActionResult BMIResult()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BMIResult(Student student)
        {
            double? height = student.Height / 100;
            double? weight = student.Weight;
            double? bmi = weight / (height * height);
            string bmiStr = "";

            if (bmi < 18.5) bmiStr = "gầy";
            else if (bmi < 24.9) bmiStr = "bình thường";
            else if (bmi < 29.9) bmiStr = "hơi béo";
            else if (bmi >= 30) bmiStr = "quá béo";

            ViewBag.Message = $"BMI là:  {bmiStr} ({bmi:0.0})";
            return View();
        }  
    }
}