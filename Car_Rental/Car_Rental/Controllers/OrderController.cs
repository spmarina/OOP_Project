using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Car_Rental.Data;
using Car_Rental.Models;
using Microsoft.VisualBasic;

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
            char[] arr;
            //Проверка фамилии
            arr = lastName.ToCharArray();
            foreach (char c in arr)
            {
                if (((c < 65) || (c > 90)) && ((c < 97) || (c > 122)))
                {

                    return RedirectToAction("Error", "Order");
                }

            }
            //Проверка имени
            arr = firstName.ToCharArray();
            foreach (char c in arr)
            {
                if (((c < 65) || (c > 90)) && ((c < 97) || (c > 122)))
                {

                    return RedirectToAction("Error", "Order");
                }

            }
            //Проверка отчества
            arr = middleName.ToCharArray();
            foreach (char c in arr)
            {
                if (((c < 65) || (c > 90)) && ((c < 97) || (c > 122)))
                {

                    return RedirectToAction("Error", "Order");
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

                    return RedirectToAction("Error", "Order");
                }
            }
            if (k < 11)
            {
                return RedirectToAction("Error", "Order");
            }
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
                    if (userCust.ActiveRent) { return RedirectToAction("Index", "Order"); } 
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
                return RedirectToAction("Error", "Order");
            }
            if (userCar.Availability == false)
            {
                return RedirectToAction("Error", "Order");
            }
            userCar.Availability = false;
            await _context.SaveChangesAsync();

            //Создание аренды
            DateTime thisDay = DateTime.Today;
            var userRent = new Rent
            {
                Customers_ID = userCust.Customers_ID,
                Cars_ID = car_ID,
                FirstDate=thisDay,
                LastDate=lastDate,
            };
            if (lastDate < thisDay)
            {
                return RedirectToAction("Error", "Order");
            }
            _context.Add(userRent);
            await _context.SaveChangesAsync();

            //Создание договора
            var checkcontract = _context.Contracts.FirstOrDefault(n => n.Number == number);
            if (checkcontract != null)
            {
                return RedirectToAction("Error", "Order");
            }
            var userContract = new Contract
            {
                Customers_ID=userCust.Customers_ID,
                Number=number,
                CreateDate=thisDay,
                Cards_ID=car_ID,
                DocumentLink=documentLink,
            };
            _context.Add(userContract);
            await _context.SaveChangesAsync();

            //Создание продажи для админа
            var userAdmin = _context.Admins.FirstOrDefault(a => a.CreateLogin == username);
            if (userAdmin == null)
            {
                return RedirectToAction("Error", "Order");
            }

            var userDiscount = _context.Discounts.FirstOrDefault(d=>d.Cars_ID==car_ID);     //для проверки скидки
            int discountCar = 0;
            if (userDiscount != null)
            {
                discountCar = (int)userCar.Price * userDiscount.NewPrice/100;
            }
            int days =  (lastDate.Month*30 +lastDate.Day)-(thisDay.Month*30+thisDay.Day);
            userAdmin.Sales += ((int)userCar.Price - discountCar)*days;
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Menu");
        }
        public IActionResult Order()
        {
            return View();
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
