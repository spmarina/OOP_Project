using System.ComponentModel.DataAnnotations;

namespace Car_Rental.Models
{
    public class Car
    {
        [Key]
        public int Cars_ID { get; set; }
        public string? Brand { 
            get; 
            set; }
        public string ?Model { get; set; }
        public decimal? Price { get; set; }
        public bool Availability { get; set; }
    }
}
