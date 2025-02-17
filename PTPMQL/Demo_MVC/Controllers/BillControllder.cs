using DemoMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoMVC.Controllers
{
    public class BillController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult BillResult()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BillResult(Bill bill)
        {
            if (bill.amount == 0 || bill.price == 0)
            {
                ViewBag.Message = "Vui lòng nhập đầy đủ thông tin";
                return View();
            }

            ViewBag.Message = "Tổng tiền đơn hàng là: " + (bill.amount * bill.price);
            return View();
        }
    }
}