using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Car_Rental.Data;
using Car_Rental.Models;
using Car_Rental.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Car_Rental.DtoModels.Login;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Microsoft.AspNetCore.Diagnostics;

namespace Car_Rental.Controllers
{
   
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        public IActionResult Index()
        {
            CurrentAdmin.id = 0;
            return View();
        }
        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> db()
        {
            return View(await _context.Admins.ToListAsync());
        }
        public IActionResult Register()
        {
            CurrentAdmin.id = 0;
            return View();
        }
        public IActionResult Error()
        {
            return View();
        }
        public IActionResult Error1()
        {
            return View();
        }
        //REGISTER

        [HttpPost]
        public async Task<IActionResult> Register(string? username, string? password)
        {
            
            if(username==null||password==null)
                return RedirectToAction("Error", "Admin");
            var isUserExists = await _context.Admins.FirstOrDefaultAsync(u => u.CreateLogin == username);
            if (isUserExists != null)
            {
                ModelState.AddModelError("", "Пользователь с таким Логином уже существует");
                return RedirectToAction("Error1", "Admin");
            }
            var newUser = new Admin
            {
                CreateLogin = username,
                CreatePassword = password,
                Sales = 0,
            };
            _context.Admins.Add(newUser);
            _context.SaveChanges();

            return RedirectToAction("db", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> Login(string? username, string? password, string? returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                
                if (await _context.Admins.AnyAsync(x => x.CreateLogin == username))
                {
                    var checkuser = await _context.Admins.FirstOrDefaultAsync(x => x.CreateLogin == username);  
                    if (checkuser != null)
                    {
                        var currUser = new CurrentAdmin(checkuser.Admins_ID);
                        
                        if (username == "Vasiliy" && password == "100")
                            return RedirectToAction("HRMenu", "Menu");
                        else
                            if (checkuser.CreatePassword == password)
                        {
                            return RedirectToAction("Index", "Menu");
                        }
                    }
                }
                
            }
            return RedirectToAction("Error", "Admin");
        }

        //public async Task<IActionResult> Logout()
        //{
        //    // Вызываем метод SignOutAsync для очистки аутентификационных кук
        //    //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        //    // Перенаправляем пользователя на главную страницу или на другую страницу
        //    return RedirectToAction("Login", "Admin");
        //}
    }
}
