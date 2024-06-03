
using Microsoft.AspNetCore.Mvc;
using Car_Rental.Data;
using Car_Rental.Models;

namespace Car_Rental.Controllers
{

    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IActionResult Index()
        {
            return View();
        }
        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return RedirectToAction("Index", "Menu");
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var admin = _context.Admins.FirstOrDefault(a => a.CreateLogin == email && a.CreatePassword == password);
            if (admin != null)
            {
                // Логика успешной авторизации (например, установка куки)
                return RedirectToAction("Index", "Menu");
            }
            else
            {
                // Логика неудачной авторизации
                ViewBag.ErrorMessage = "Invalid email or password";
                return View();
            }
        }

        //[HttpPost]
        //public IActionResult Login(string email, string password)
        //{
        //    var admin = _context.Admins.FirstOrDefault(a => a.CreateLogin == email && a.CreatePassword == password); // Assuming you have a Password field
        //    if (admin == null)
        //    {
        //        return Json(new { success = false, message = "Invalid email or password" });
        //    }

        //    // Add your login logic here (e.g., setting cookies, session variables, etc.)

        //    return Json(new { success = true, message = "Login successful" });
        //}

        [HttpGet]
        public IActionResult Register()
        {
            if (ModelState.IsValid)
            {
                //_context.Admins.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Login","Admin");
            }
            return RedirectToAction("Login", "Admin");
        }

        //[HttpPost]
        //public IActionResult Register(Admin model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Admins.Add(model);
        //        _context.SaveChanges();
        //        return RedirectToAction("Login");
        //    }
        //    return View(model);
        //}
    }
}
