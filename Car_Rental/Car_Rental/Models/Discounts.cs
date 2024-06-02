using System.ComponentModel.DataAnnotations.Schema;

namespace Car_Rental.Models
{
    public class Discounts
    {
        public int Discounts_ID { get; set; }
        [ForeignKey ("Car")]
        public int Cars_ID { get; set; }    //Car
        public int NewPrice { get; set; }
    }
}
