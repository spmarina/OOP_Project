using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Car_Rental.Models
{
    public class Card
    {
        [Key]
        public int Cards_ID { get; set; }
        [ForeignKey("Customer")]
        public int Customers_ID { get; set; }  //Customer
        public int Cashback {  get; set; }
        public int Points {  get; set; }
        public int Payment { get; set;}
    }
}
