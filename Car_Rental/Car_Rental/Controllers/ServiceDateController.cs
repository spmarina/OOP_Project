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
    public class ServiceDateController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServiceDateController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ServiceDate
        public async Task<IActionResult> Index()
        {
            return View(await _context.ServicesDates.ToListAsync());
        }

        // GET: ServiceDate/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceDate = await _context.ServicesDates
                .FirstOrDefaultAsync(m => m.ServiceDate_ID == id);
            if (serviceDate == null)
            {
                return NotFound();
            }

            return View(serviceDate);
        }

        // GET: ServiceDate/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ServiceDate/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServiceDate_ID,Cars_ID,PreviousDate,NextDate")] ServiceDate serviceDate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(serviceDate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(serviceDate);
        }

        // GET: ServiceDate/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceDate = await _context.ServicesDates.FindAsync(id);
            if (serviceDate == null)
            {
                return NotFound();
            }
            return View(serviceDate);
        }

        // POST: ServiceDate/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServiceDate_ID,Cars_ID,PreviousDate,NextDate")] ServiceDate serviceDate)
        {
            if (id != serviceDate.ServiceDate_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(serviceDate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceDateExists(serviceDate.ServiceDate_ID))
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
            return View(serviceDate);
        }

        // GET: ServiceDate/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceDate = await _context.ServicesDates
                .FirstOrDefaultAsync(m => m.ServiceDate_ID == id);
            if (serviceDate == null)
            {
                return NotFound();
            }

            return View(serviceDate);
        }

        // POST: ServiceDate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var serviceDate = await _context.ServicesDates.FindAsync(id);
            if (serviceDate != null)
            {
                _context.ServicesDates.Remove(serviceDate);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceDateExists(int id)
        {
            return _context.ServicesDates.Any(e => e.ServiceDate_ID == id);
        }
    }
}
