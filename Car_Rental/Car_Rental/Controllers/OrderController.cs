using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Car_Rental.Data;
using Car_Rental.Models;

namespace Car_Rental.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        public IActionResult Index()
        {
            return View();
        }
        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult Order(string LastName, string FirstName, string MiddleName, string Phone, int Car_ID, DateTime LastDate, int Number, string DocumentLink, string username)
        {
            return View();
        }
        public IActionResult Order()
        {
            return View();
        }
    }
}
