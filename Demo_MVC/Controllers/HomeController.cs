using Microsoft.AspNetCore.Mvc;
using my_mvc_app.Models;

namespace my_mvc_app.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var model = new HomeModel();
            return View(model);
        }
    }
}