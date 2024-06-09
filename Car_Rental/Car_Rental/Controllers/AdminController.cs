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
    //[Route("api/[controller]")]
    //[ApiController]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        public IActionResult Index(string? returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        //REGISTER

        [HttpGet]
        public async Task<IActionResult> Register(string username, string password, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var isUserExists = await _context.Admins.FirstOrDefaultAsync(u => u.CreateLogin == username && u.CreatePassword == password);
            if (isUserExists != null)
            {
                ModelState.AddModelError("", "Пользователь с таким Логином уже существует");
                return RedirectToAction("Index", "Admin");
            }
            var newUser = new Admin
            {
                CreateLogin = username,
                CreatePassword = password,
                Sales = 0,
            };
            _context.Admins.Add(newUser);
            _context.SaveChanges();

            return RedirectToAction("Index", "Admin");
        }

  

        [HttpPost]
        public async Task<IActionResult> RegisterPost(string username, string password, string? returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var isUserExists = await _context.Admins.FirstOrDefaultAsync(u => u.CreateLogin == username && u.CreatePassword == password);
            if (isUserExists != null)
            {
                ModelState.AddModelError("", "Пользователь с таким email уже существует");
                return RedirectToAction("Index", "Admin");
            }
            var newUser = new Admin
            {
                CreateLogin = username,
                CreatePassword = password,
                Sales = 0,
            };
            _context.Admins.Add(newUser);
            _context.SaveChanges();

            return RedirectToAction("Index", "Admin");
        }

        private IActionResult RedirectToLocal(string? returnUrl)
        {

            ViewData["ReturnUrl"] = returnUrl;
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                // Перенаправление на MainMenu 
                return RedirectToAction("Index", "Menu");
            }
        }





        [HttpGet]
        public async Task<IActionResult> LoginPost(string username, string password, string? returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (await _context.Admins.AnyAsync(x => x.CreateLogin == username))
            {
                var checkuser = await _context.Admins.FirstOrDefaultAsync(x => x.CreateLogin == username);
                if (checkuser != null)
                {
                    if (checkuser.CreatePassword == password)
                    {
                        return RedirectToAction("Index", "Menu");
                    }
                }
            }
            return RedirectToAction("Index", "Menu");
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password, string? returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {

                if (await _context.Admins.AnyAsync(x => x.CreateLogin == username))
                {
                    var checkuser = await _context.Admins.FirstOrDefaultAsync(x => x.CreateLogin == username);  
                    if (checkuser != null)
                    {
                        if (username == "Michail" && password == "12345678")
                            return RedirectToAction("HRMenu", "Menu");
                        else
                        if (checkuser.CreatePassword == password)
                        {
                            return RedirectToAction("Index", "Menu");
                        }
                    }
                }
                
            }
            return RedirectToAction("Index", "Admin");
        }

        public async Task<IActionResult> Logout()
        {
            // Вызываем метод SignOutAsync для очистки аутентификационных кук
            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Перенаправляем пользователя на главную страницу или на другую страницу
            return RedirectToAction("Login", "Admin");
        }
    }
}
