using Car_Rental.Data;
using Car_Rental.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Car_Rental.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    public class CarController1 : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CarController1(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Car
        [HttpGet]
        
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            return await _context.Cars.ToListAsync();
        }

        // GET: api/Car/search?query=value
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Car>>> SearchCars(string query)
        {
            return await _context.Cars
                .Where(c => c.Brand.Contains(query) || c.Model.Contains(query))
                .ToListAsync();
        }

        // GET: api/Car/filter?brand=value&isAvailable=value
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<Car>>> FilterCars(string brand, bool? isAvailable)
        {
            var cars = _context.Cars.AsQueryable();

            if (!string.IsNullOrEmpty(brand))
            {
                cars = cars.Where(c => c.Brand == brand);
            }

            if (isAvailable.HasValue)
            {
                cars = cars.Where(c => c.Availability == isAvailable.Value);
            }

            return await cars.ToListAsync();
        }

        // GET: api/Car/brands
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<string>>> GetBrands()
        {
            return await _context.Cars
                .Select(c => c.Brand)
                .Distinct()
                .ToListAsync();
        }
    }
    public class CarController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}