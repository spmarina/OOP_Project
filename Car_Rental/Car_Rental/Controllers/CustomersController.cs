using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Car_Rental.Data;
using Car_Rental.Models;
using Car_Rental.Controllers;

namespace Car_Rental.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var currUser = new CurrentAdmin();
            if (CurrentAdmin.id == 0)
            {
                return RedirectToAction("Index", "Admin");
            }
            return View(await _context.Customers.ToListAsync());
        }

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

        [HttpPost]
        public async Task<IActionResult> Create(string lastName, string firstName, string middleName, string phone, bool activeRent)
        {
            var currUser = new CurrentAdmin();
            if (CurrentAdmin.id == 0)
            {
                return RedirectToAction("Index", "Admin");
            }
            var CreateCustomer = new Customer
            {
                LastName = lastName,
                FirstName = firstName,
                MiddleName = middleName,
                Phone = phone,
                ActiveRent = activeRent
            };
            var checkPhone=_context.Customers.FirstOrDefault(_context => _context.Phone == phone);
            if (checkPhone != null)
            {
                return RedirectToAction("Error", "Customers");
            }
            char[] arr;
            //Проверка фамилии
            arr = lastName.ToCharArray();
            foreach (char c in arr)
            {
                if (((c < 65) || (c > 90)) && ((c < 97) || (c > 122)))
                {
                    return RedirectToAction("Error", "Customers");
                }

            }
            //Проверка имени
            arr = firstName.ToCharArray();
            foreach (char c in arr)
            {
                if (((c < 65) || (c > 90)) && ((c < 97) || (c > 122)))
                {
                    return RedirectToAction("Error", "Customers");
                }

            }
            //Проверка отчества
            arr = middleName.ToCharArray();
            foreach (char c in arr)
            {
                if (((c < 65) || (c > 90)) && ((c < 97) || (c > 122)))
                {
                    return RedirectToAction("Error", "Customers");
                }

            }
            //Проверка телефона
            arr = phone.ToCharArray();
            int k = 0;
            foreach (char c in arr)
            {
                k++;
                if ((c < 48) || (c > 57)||(k>11))
                {
                    return RedirectToAction("Error", "Customers");
                }
            }
            if (k < 11)
            {
                return RedirectToAction("Error", "Customers");
            }
            //Проферка True/False
            if (ModelState.IsValid)
            {
                _context.Add(CreateCustomer);
                await _context.SaveChangesAsync();
                CreateCardForCustomer(CreateCustomer.Customers_ID);
                
                return RedirectToAction("Index", "Customers");
            }
            return RedirectToAction("Error", "Customers");
        }
        public IActionResult Error()
        {
            return View();
        }
        public void CreateCardForCustomer(int customers_ID)
        {
            Random r = new Random();
            int rInt = r.Next(0, 25);
            var CreateCard = new Card
            {
                Customers_ID = customers_ID,
                Cashback = rInt,
                Points = 0,
                Payment = 0,
            };

            _context.Add(CreateCard);
            _context.SaveChangesAsync();
        }
    }
}
