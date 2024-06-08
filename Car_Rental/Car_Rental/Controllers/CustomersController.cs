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
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Customers
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

        // GET: Customers/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
            try
            {
                int lastNameInt = Convert.ToInt32(lastName);
                if (lastNameInt > 0)
                {
                    return View(CreateCustomer);
                }
            }
            catch (FormatException) {
                try
                {
                    int firstNameInt = Convert.ToInt32(firstName);
                    if (firstNameInt > 0) { return View(CreateCustomer); }
                }
                catch (FormatException)
                {
                    try
                    {
                        int middleNameInt = Convert.ToInt32(middleName);
                        if (middleNameInt > 0) { return View(CreateCustomer); }
                    }
                    catch (FormatException)
                    {
                        //string telephone = phone;
                        //string area = phone.Substring(0, 3);
                        //string major = phone.Substring(3, 3);
                        //string minor = phone.Substring(6);
                        //string formatted = string.Format("{0}-{1}-{2}", area, major, minor);

                        if (ModelState.IsValid)
                        {
                            _context.Add(CreateCustomer);
                            await _context.SaveChangesAsync();
                            return RedirectToAction("Create", "Customers");
                        }
                    }
                }
            }
            return View(CreateCustomer);
        }



        // GET: Customers/Edit/5
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

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Customers/Delete/5
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

        // POST: Customers/Delete/5
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
