using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Car_Rental.Models
{
    [Table("Rents")]
    public class Rent
    {
        [Key]
        public int Rent_ID { get; set; }
        [ForeignKey("Customer")]
        public int Customers_ID { get; set; }       //Customer
        [ForeignKey("Car")]
        public int Cars_ID { get; set;}     //Car
        public DateTime FirstDate { get; set; }
        public DateTime LastDate { get; set; }

    }
}
