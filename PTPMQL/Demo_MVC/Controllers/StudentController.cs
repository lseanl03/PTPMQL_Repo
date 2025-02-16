using DemoMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoMVC.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            Student std = new Student();
            std.Id = "1";
            std.FullName = "Nguyen Van A";
            return View(std);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student std)
        {
            ViewBag.Message = "ID:" + std.Id + " FullName:" + std.FullName;
            return View();
        }
    }
}