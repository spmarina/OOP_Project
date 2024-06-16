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
    public class RentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RentController(ApplicationDbContext context)
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
            return View(await _context.Rents.ToListAsync());
        }

     
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Rent_ID,Customers_ID,Cars_ID,FirstDate,LastDate")] Rent rent)
        {
            var currUser = new CurrentAdmin();
            if (CurrentAdmin.id == 0)
            {
                return RedirectToAction("Index", "Admin");
            }
            if (ModelState.IsValid)
            {
                _context.Add(rent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rent);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rent = await _context.Rents.FindAsync(id);
            if (rent == null)
            {
                return NotFound();
            }
            return View(rent);
        }

    }
}
