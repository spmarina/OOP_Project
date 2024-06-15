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
            return View(await _context.Customers.ToListAsync());
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.Customers_ID == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string lastName, string firstName, string middleName, string phone, bool activeRent)
        {
            var CreateCustomer = new Customer
            {
                LastName = lastName,
                FirstName = firstName,
                MiddleName = middleName,
                Phone = phone,
                ActiveRent = activeRent
            };
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
        

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Customers_ID,LastName,FirstName,MiddleName,Phone,ActiveRent")] Customer customer)
        {
            if (id != customer.Customers_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Customers_ID))
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
            return View(customer);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.Customers_ID == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Customers_ID == id);
        }
    }
}
