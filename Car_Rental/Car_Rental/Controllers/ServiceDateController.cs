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
            var currUser = new CurrentAdmin();
            if (CurrentAdmin.id == 0)
            {
                return RedirectToAction("Index", "Admin");
            }
            return View(await _context.ServicesDates.ToListAsync());
        }


        // GET: ServiceDate/Create
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> Create(int cars_ID, DateTime PreviousDate, DateTime NextDate)
        {
            var currUser = new CurrentAdmin();
            if (CurrentAdmin.id == 0)
            {
                return RedirectToAction("Index", "Admin");
            }
            var createServiceDate = new ServiceDate
            {
                Cars_ID = cars_ID,
                PreviousDate = PreviousDate,
                NextDate = NextDate
            };
            var checkcar = await _context.Cars.FirstOrDefaultAsync(x => x.Cars_ID == cars_ID);
            if (checkcar == null)
            {
                return RedirectToAction("Error", "ServiceDate");
            }
            if (ModelState.IsValid)
            {
                _context.Add(createServiceDate);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "ServiceDate");
            }
            return RedirectToAction("Error", "ServiceDate") ;
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
