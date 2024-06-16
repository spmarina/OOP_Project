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
            var currUser = new CurrentAdmin();
            if (CurrentAdmin.id == 0)
            {
                return RedirectToAction("Index", "Admin");
            }
            return View(await _context.Cars.ToListAsync());


        }

        // GET: Customers/Calendar
        [HttpGet]
        public IActionResult Calendar()
        {
            var currUser = new CurrentAdmin();
            if (CurrentAdmin.id == 0)
            {
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }

       
        public IActionResult Error()
        {
            return View();
        }
        // GET: Car/Create
        [HttpGet]
        public IActionResult Create()
        {
            var currUser = new CurrentAdmin();
            if (CurrentAdmin.id == 0)
            {
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }

        // POST: Car/Create
        [HttpPost]
        public async Task<IActionResult> Create(string brand, string model, decimal price, bool availability)
        {
            var currUser = new CurrentAdmin();
            if (CurrentAdmin.id == 0)
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
                    return RedirectToAction("Error", "Car"); ;
                }

            }
            //Проверка модели
            arr = model.ToCharArray();
            foreach (char c in arr)
            {
                if (((c < 65) || (c > 90)) && ((c < 97) || (c > 122))&& ((c < 48) || (c > 57))&&(c!=32))
                {
                    return RedirectToAction("Error", "Car"); ;
                }

            }
            //Проевкри стоимость
            if (price <= 0 || price > 999999999)
            {
                return RedirectToAction("Error", "Car");
            }
            
            //Проферка True/False
            if (ModelState.IsValid)
            {
                _context.Add(CreateCar);
                await _context.SaveChangesAsync();
                createServiceDate(CreateCar.Cars_ID);
                return RedirectToAction("Index", "Car");
            }
            return RedirectToAction("Error", "Car");
        }

        public  void createServiceDate(int car_id)
        {
            var newServiceDate = new ServiceDate
            {
                Cars_ID = car_id,
                PreviousDate = DateTime.Now,
                NextDate = DateTime.UtcNow.AddMonths(1),
            };
            _context.Add(newServiceDate);
             _context.SaveChangesAsync();
            
        }


        
    }
}
