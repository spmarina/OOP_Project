using System.ComponentModel.DataAnnotations;

namespace Car_Rental.Models
{
    public class Customer
    {
        [Key]
        public int Customers_ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Phone { get; set; }
        public bool ActiveRent {  get; set; }

    }
}
