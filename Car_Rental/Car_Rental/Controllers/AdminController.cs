
using Microsoft.AspNetCore.Mvc;
using Car_Rental.Data;
using Car_Rental.Models;

namespace Car_Rental.Controllers
{

    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var admin = _context.Admins.FirstOrDefault(a => a.CreateLogin == email && a.CreatePassword == password);
            if (admin != null)
            {
                // Логика успешной авторизации (например, установка куки)
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Логика неудачной авторизации
                ViewBag.ErrorMessage = "Invalid email or password";
                return View();
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Admin model)
        {
            if (ModelState.IsValid)
            {
                _context.Admins.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(model);
        }
    }
}
