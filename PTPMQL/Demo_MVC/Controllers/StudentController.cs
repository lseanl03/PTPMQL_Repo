using Demo_MVC.Models;
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
        
        [HttpGet]
        public IActionResult BMIResult()
        {
            return View();
        }

        public IActionResult BMIResult(BMI bMI)
        {
            if(bMI.Weight == 0 || bMI.Height == 0 || bMI.Gender == null)
            {
                ViewBag.Message = "Vui lòng nhập đầy đủ thông tin";
                return View();
            }
            string result = "";
            double mHeight = bMI.Height / 100; 
            bMI.Result = (int)(bMI.Weight / (mHeight * mHeight));
            if(bMI.Result < 18.5) result = "Bạn đang gầy";
            else if(bMI.Result < 24.9) result = "Bạn đang bình thường";
            else if(bMI.Result < 29.9) result = "Bạn đang thừa cân";
            else if(bMI.Result < 34.9) result = "Bạn đang béo phì cấp độ 1";
            else if(bMI.Result < 39.9) result = "Bạn đang béo phì cấp độ 2";
            else ViewBag.Message = "Bạn đang béo phì cấp độ 3";

            ViewBag.Message = "Giới tính: " + bMI.Gender + " Kết quả BMI: " + bMI.Result + " Đánh giá: " + result;   
            return View();
        }
        
        [HttpGet]
        public IActionResult SubjectResult()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SubjectResult(Subject subject)
        {
            if (subject.a == 0 || subject.b == 0 || subject.c == 0)
            {
                ViewBag.Message = "Vui lòng nhập đầy đủ thông tin";
                return View();
            }
            float a = subject.a * 0.6f;
            float b = subject.b * 0.3f;
            float c = subject.c * 0.1f;
            ViewBag.Message = "Tổng điểm môn học là: " + (a + b + c);
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