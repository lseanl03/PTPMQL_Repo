using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Demo_MVC.Models;

namespace Demo_MVC.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}