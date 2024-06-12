using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Car_Rental.Models
{
    public class Discount
    {
        [Key]
        public int Discounts_ID { get; set; }
        [ForeignKey ("Car")]
        public int Cars_ID { get; set; }    //Car
        public byte NewPrice { get; set; }
        [ForeignKey("Car")]
        public string Model { get; set; }
    }
}
