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
    public class DiscountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DiscountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Discount
        public async Task<IActionResult> Index()
        {
            var currUser = new CurrentAdmin();
            if (CurrentAdmin.id == 0)
            {
                return RedirectToAction("Index", "Admin");
            }
            return View(await _context.Discounts.ToListAsync());
        }

        
        // GET: Discount/Create
        public IActionResult Create()
        {
            var currUser = new CurrentAdmin();
            if (CurrentAdmin.id == 0)
            {
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }

        // POST: Discount/Create
        [HttpPost]
        public async Task<IActionResult> Create(int cars_ID,byte new_price)
        {
            var currUser = new CurrentAdmin();
            if (CurrentAdmin.id == 0)
            {
                return RedirectToAction("Index", "Admin");
            }
            var createDiscount = new Discount
            {
                Cars_ID = cars_ID,
                NewPrice=new_price,

            };
            if (new_price <= 0 || new_price > 25)
            {
                return RedirectToAction("Error", "Discount");
            }
            var checkcar = await _context.Cars.FirstOrDefaultAsync(x => x.Cars_ID == cars_ID);
            if(checkcar == null)
            {
                return RedirectToAction("Error", "Discount");
            }
            else
            {
                createDiscount.Model = checkcar.Model;
            }
            var checkDiscount= await _context.Discounts.FirstOrDefaultAsync(x => x.Cars_ID == cars_ID);
            if (checkDiscount != null)
            {
                return RedirectToAction("Error", "Discount");
            }

            if (ModelState.IsValid)
            {
                _context.Add(createDiscount);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Discount");
            }
            return RedirectToAction("Error", "Discount");
        }
        public IActionResult Error()
        {
            return View();
        }
        
        // GET: Discount/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();//Ошибка что не найдена
            }

            var discount = await _context.Discounts
                .FirstOrDefaultAsync(m => m.Discounts_ID == id);
            if (discount == null)
            {
                return NotFound();//Ошибка что не найдена
            }

            return View(discount);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var discount = await _context.Discounts.FindAsync(id);
            if (discount != null)
            {
                _context.Discounts.Remove(discount);
            }
            else
            {
                return NotFound();//Ошибка что не найдена
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Discount");
        }

    }
}
