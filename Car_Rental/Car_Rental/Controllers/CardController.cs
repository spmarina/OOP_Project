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
    public class CardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CardController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Card
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cards.ToListAsync());
        }

        // GET: Card/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = await _context.Cards
                .FirstOrDefaultAsync(m => m.Cards_ID == id);
            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        // GET: Card/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Card/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
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
                    return View(CreateCustomer);
                }

            }
            //Проверка имени
            arr = firstName.ToCharArray();
            foreach (char c in arr)
            {
                if (((c < 65) || (c > 90)) && ((c < 97) || (c > 122)))
                {
                    return View(CreateCustomer);
                }

            }
            //Проверка отчества
            arr = middleName.ToCharArray();
            foreach (char c in arr)
            {
                if (((c < 65) || (c > 90)) && ((c < 97) || (c > 122)))
                {
                    return View(CreateCustomer);
                }

            }
            //Проверка телефона
            arr = phone.ToCharArray();
            int k = 0;
            foreach (char c in arr)
            {
                k++;
                if ((c < 48) || (c > 57) || (k > 11))
                {
                    return View(CreateCustomer);
                }
            }
            if (k < 11)
            {
                return View(CreateCustomer);
            }
            //Проферка True/False
            if (ModelState.IsValid)
            {
                _context.Add(CreateCustomer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "Customers");
            }
            return View(CreateCustomer);
        }

        // GET: Card/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = await _context.Cards.FindAsync(id);
            if (card == null)
            {
                return NotFound();
            }
            return View(card);
        }

        // POST: Card/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Cards_ID,Customers_ID,Cashback,Points,Payment")] Card card)
        {
            if (id != card.Cards_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(card);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CardExists(card.Cards_ID))
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
            return View(card);
        }

        // GET: Card/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = await _context.Cards
                .FirstOrDefaultAsync(m => m.Cards_ID == id);
            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        // POST: Card/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var card = await _context.Cards.FindAsync(id);
            if (card != null)
            {
                _context.Cards.Remove(card);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CardExists(int id)
        {
            return _context.Cards.Any(e => e.Cards_ID == id);
        }
    }
}
