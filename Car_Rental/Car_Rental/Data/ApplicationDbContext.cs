using Car_Rental.Models;
using Microsoft.EntityFrameworkCore;

namespace Car_Rental.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Rent> Rents { get; set; }
        public DbSet<ServiceDate> ServicesDates { get; set; }
    }
}
