using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Car_Rental.Models
{
    public class ServiceDate
    {
        [Key]
        public int ServiceDate_ID { get; set; }
        [ForeignKey ("Car")]
        public int Cars_ID { get; set; }    // Car
        public DateTime PreviousDate { get; set; }
        public DateTime NextDate { get; set; }
    }
}
