﻿using System;
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
            return View();
        }
        public ActionResult Customers()
        {
            return View();
        }

        // GET: MenuController/Details/5
        public ActionResult Car()
        {
            
            return View();
        }

        // GET: MenuController/Create
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
            return View();
        }
        // POST: MenuController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MenuController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MenuController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MenuController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MenuController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
