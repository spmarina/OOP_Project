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
        public async Task<IActionResult> Order(string lastName, string firstName, string middleName, string phone, int car_ID, DateTime lastDate, int number, string documentLink, string username)
        {
            //Проверка на покупателя
            var createOrderCustomers = new Customer
            {
                LastName = lastName,
                FirstName = firstName,
                MiddleName = middleName,
                Phone = phone,
            };
            var userCust = _context.Customers.FirstOrDefault(u=>u.LastName==lastName);
            if (userCust == null)
            {
                var newuserCust = new Customer
                {
                    LastName = lastName,
                    FirstName = firstName,
                    MiddleName = middleName,
                    Phone = phone,
                    ActiveRent = true,
                };
                _context.Add(newuserCust);
                await _context.SaveChangesAsync();
                //return RedirectToAction("Index", "Order");
            }
            else
            {
                if ((userCust.FirstName == firstName) && (userCust.MiddleName == middleName) && (userCust.Phone == phone))
                {
                    ;
                }
                else
                {
                    _context.Add(userCust);
                    await _context.SaveChangesAsync();
                }
            }
            //Проверка на машину
            var userCar=_context.Cars.FirstOrDefault(c=>c.Cars_ID==car_ID);
            if(userCar == null)
            {
                return RedirectToAction("Index", "Order");
            }
            if (userCar.Availability == false)
            {
                return RedirectToAction("Index", "Order");
            }

            //Создание аренды
            DateTime thisDay = DateTime.Today;
            var userRent = new Rent
            {
                Customers_ID = userCust.Customers_ID,
                Cars_ID = car_ID,
                FirstDate=thisDay,
                LastDate=lastDate,
            };
            _context.Add(userRent);
            await _context.SaveChangesAsync();

            //Создание договора
            var checkcontract = _context.Contracts.FirstOrDefault(n => n.Number == number);
            if (checkcontract != null)
            {
                return RedirectToAction("Index", "Order");
            }
            var userContract = new Contract
            {
                Customers_ID=userCust.Customers_ID,
                Number=number,
                CreateDate=thisDay,
                Cards_ID=car_ID,
            };
            _context.Add(userContract);
            _context.SaveChangesAsync();

            //Создание продажи для админа
            var userAdmin = _context.Admins.FirstOrDefault(a => a.CreateLogin == username);
            if (userAdmin == null)
            {
                return RedirectToAction("Index", "Order");
            }

            var userDiscount = _context.Discounts.FirstOrDefault(d=>d.Cars_ID==car_ID);     //для проверки скидки
            int discountCar = 0;
            if (userDiscount != null)
            {
                discountCar = (int)userCar.Price * userDiscount.NewPrice/100;
            }
            userAdmin.Sales += (int)userCar.Price - discountCar;
            await _context.SaveChangesAsync();
            return View();
        }
        public IActionResult Order()
        {
            return View();
        }
    }
}
