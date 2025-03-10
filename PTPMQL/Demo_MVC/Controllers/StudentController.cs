using Demo_MVC.Models;
using DemoMVC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Demo_MVC.Models.Process;

namespace DemoMVC.Controllers
{
    public class StudentController : Controller
    {
        //khai bao dbcontext
        private readonly ApplicationDbContext _context;
        private ExcelProcess _excelProcess = new ExcelProcess();
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

        public async Task<IActionResult> Upload()
        {
            return View();
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if(file != null){
                string fileExtension = Path.GetExtension(file.FileName);
                if (fileExtension != ".xls" && fileExtension != ".xlsx")
                {
                    ModelState.AddModelError("", "Please choose an Excel file to upload!");
                }
                else{
                    var fileName = DateTime.Now.ToShortTimeString() + fileExtension;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/Excels", fileName);
                    var fileLocation = new FileInfo(filePath).ToString();
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                        var dt = _excelProcess.ReadExcelToDataTable(fileLocation);
                        for(int i = 0; i < dt.Rows.Count; i++){
                            var student = new Student
                            {
                                Id = int.Parse(dt.Rows[i][0].ToString()),
                                FullName = dt.Rows[i][1].ToString(),
                                Email = dt.Rows[i][2].ToString(),
                            };
                            _context.Add(student);
                        }
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View();
        }
    }
}