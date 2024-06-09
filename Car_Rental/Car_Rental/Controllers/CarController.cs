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
    public class CarController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Car
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Admin");
            }
            return View(await _context.Cars.ToListAsync());
        }

        // GET: Car/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Admin");
            }
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .FirstOrDefaultAsync(m => m.Cars_ID == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Car/Create
        [HttpGet]
        public IActionResult Create()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }

        // POST: Car/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string brand, string model, decimal price, bool availability)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Admin");
            }
            var CreateCar = new Car
            {
                Brand = brand,
                Model = model,
                Price = price,
                Availability = availability
            };
            char[] arr;
            //Проверка марки
            arr = brand.ToCharArray();
            foreach (char c in arr)
            {
                if (((c < 65) || (c > 90)) && ((c < 97) || (c > 122)))
                {
                    return View(CreateCar);
                }

            }
            //Проверка модели
            arr = model.ToCharArray();
            foreach (char c in arr)
            {
                if (((c < 65) || (c > 90)) && ((c < 97) || (c > 122))&& ((c < 48) || (c > 57))&&(c!=32))
                {
                    return View(CreateCar);
                }

            }
            //Проевкри стоимость
            if (price <= 0 || price > 999999999)
            {
                return View(CreateCar);
            }
            
            //Проферка True/False
            if (ModelState.IsValid)
            {
                _context.Add(CreateCar);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Car");
            }
            return View(CreateCar);
        }

        // GET: Car/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Admin");
            }
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        // POST: Car/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Cars_ID,Brand,Model,Price,Availability")] Car car)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Admin");
            }
            if (id != car.Cars_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Cars_ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET: Car/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Admin");
            }
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .FirstOrDefaultAsync(m => m.Cars_ID == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Car/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Admin");
            }
            var car = await _context.Cars.FindAsync(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            
            return _context.Cars.Any(e => e.Cars_ID == id);
        }
    }
}
