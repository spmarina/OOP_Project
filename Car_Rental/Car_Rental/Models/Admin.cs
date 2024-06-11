using System.ComponentModel.DataAnnotations;

namespace Car_Rental.Models
{
    public class Admin
    {
        [Key]
        public int Admins_ID { get; set; }
        public string CreateLogin {  get; set; }
        public string CreatePassword {  get; set; }
        public int Sales { get; set; }
    }
}
