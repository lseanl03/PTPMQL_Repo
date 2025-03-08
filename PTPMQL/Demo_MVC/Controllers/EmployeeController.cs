using Microsoft.AspNetCore.Mvc;

namespace Demo_MVC.Controllers;

public class EmployeeController : Controller {
  public IActionResult Index() {
    return View();
  }

  public IActionResult EmployeeInfo() {
    return View();
  }
}