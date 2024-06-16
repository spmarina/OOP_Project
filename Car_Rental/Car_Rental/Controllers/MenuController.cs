using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Car_Rental.Data;
using Car_Rental.Models;

namespace Car_Rental.Controllers
{
    public class MenuController : Controller
    {
        // GET: MenuController
        public async Task<IActionResult> Index()
        {
            var currUser = new CurrentAdmin();
            if (CurrentAdmin.id == 0)
            {
                return RedirectToAction("Index","Admin");
            }
            return View();
        }
        public ActionResult Customers()
        {
           
            return View();
        }

        public ActionResult Car()
        {
            
            return View();
        }

        public ActionResult Rent()
        {
            
            return View();
        }

        public ActionResult Discount()
        {
            
            return View();
        }
        public ActionResult ServiceDate()
        {
            
            return View();
        }
        public ActionResult Contract()
        {
            return View();
        }
        public ActionResult HRMenu()
        {
            var currUser = new CurrentAdmin();
            if (CurrentAdmin.id == 0)
            {
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            // Вызываем метод SignOutAsync для очистки аутентификационных кук
            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Перенаправляем пользователя на главную страницу или на другую страницу
            CurrentAdmin.id = 0;
            return RedirectToAction("Login", "Admin");
        }
    }
}
