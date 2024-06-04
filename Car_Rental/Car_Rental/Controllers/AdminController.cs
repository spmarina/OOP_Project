﻿using BCrypt.Net;
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

namespace Car_Rental.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
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
        public IActionResult Register()
        {
            return RedirectToAction("Login");
        }

        [HttpPost]
        // [Route("admin/{CreateLogin:string)/{CreatePassword:string }")]
        public async Task<IActionResult> Register(string email, string password)
        {
            //if (_context.Admins.Any(u => u.CreateLogin == email))
            //{
            //    ViewBag.ErrorMessage = "Email already registered";
            //    return View();
            //}

            var user = new Admin { CreateLogin = email, CreatePassword = BCrypt.Net.BCrypt.HashPassword(password) };
            _context.Admins.Add(user);
            await _context.SaveChangesAsync();

            return RedirectToAction("Login");
        }

        [HttpGet(nameof(Login), Name = nameof(Login))]
        public IActionResult Login()
        {
            return RedirectToAction("Index", "Menu");
            //return RedirectToAction("Login", "Admin");
        }

        [HttpPost(nameof(LoginPost), Name = nameof(LoginPost))]
        public async Task<IActionResult> LoginPost(LoginModel LogMod)
        {

            //{
            //    if (user != null && BCrypt.Net.BCrypt.Verify(password, user.CreatePassword))
            //    {
            //        // Успешная аутентификация
            //        // Установите куки или сессии здесь
            //        return RedirectToAction("Index", "Home");
            //    }
            //    else
            //    {
            //        ViewBag.ErrorMessage = "Invalid email or password";
            //        return View();
            //    }
            //}
            if (!ModelState.IsValid)
                return View(nameof(Login), LogMod);
            try
            {
                //var user = await _context.GetUserByMail(LogMod.email);
                
                ////return RedirectToAction(nameof(ManageController.Sales), "Manage", new { usId = user.IsnNode});
                //return RedirectToAction("Index", "Menu", new { usId = user.IsnNode, name = user.Name, surname = user.Surname });
                var user = _context.Admins.FirstOrDefault(u => u.CreateLogin == LogMod.CreateLogin);
                if (user == null) throw new Exception("Пользователь не найден");
                return RedirectToAction("Index", "Menu");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(nameof(Login), LogMod);
            }



        }
            //    [HttpGet]
            //    public IActionResult Login()
            //    {
            //        return RedirectToAction("Index", "Menu");
            //    }

            //    [HttpPost]
            //    public IActionResult Login(string email, string password)
            //    {
            //        var admin = _context.Admins.FirstOrDefault(a => a.CreateLogin == email && a.CreatePassword == password);


            //        if (admin != null)
            //        {
            //            // Логика успешной авторизации (например, установка куки)
            //            return RedirectToAction("Index", "Menu");
            //        }
            //        else
            //        {
            //            // Логика неудачной авторизации
            //            ViewBag.ErrorMessage = "Invalid email or password";
            //            return View();
            //        }

            //    }

            //    //[HttpPost]
            //    //public IActionResult Login(string email, string password)
            //    //{
            //    //    var admin = _context.Admins.FirstOrDefault(a => a.CreateLogin == email && a.CreatePassword == password); // Assuming you have a Password field
            //    //    if (admin == null)
            //    //    {
            //    //        return Json(new { success = false, message = "Invalid email or password" });
            //    //    }

            //    //    // Add your login logic here (e.g., setting cookies, session variables, etc.)

            //    //    return Json(new { success = true, message = "Login successful" });
            //}

            //    [HttpGet]
            //    public IActionResult Register()
            //    {
            //        if (ModelState.IsValid)
            //        {
            //            //_context.Admins.Add(model);
            //            _context.SaveChanges();
            //            return RedirectToAction("Login","Admin");
            //        }
            //        return RedirectToAction("Login", "Admin");
            //    }

            //    //[HttpPost]
            //    //public IActionResult Register(Admin model)
            //    //{
            //    //    if (ModelState.IsValid)
            //    //    {
            //    //        _context.Admins.Add(model);
            //    //        _context.SaveChanges();
            //    //        return RedirectToAction("Login");
            //    //    }
            //    //    return View(model);
            //    //}
        
    }
}
