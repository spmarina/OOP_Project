using System.ComponentModel.DataAnnotations.Schema;
namespace Car_Rental.Models
{
    public class Contract { 

        public int Contracts_ID
        {
            get; set;
        }
        [ForeignKey("Customer")]
        public int Customers_ID
        {
            get; set;
        }
        public int Number
        {
            get; set;
        }
        public DateTime CreateDate
        {
            get; set;
        }
        [ForeignKey("Card")]
        public int Cards_ID
        {
            get; set;
        }
    }
}
