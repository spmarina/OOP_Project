using System.ComponentModel.DataAnnotations;

namespace Car_Rental.Models
{
    public class CurrentAdmin
    {

        public static int id = 0;
        public CurrentAdmin(int ID)
        {
            id= ID;
        }
        public CurrentAdmin()
        {
            ;
        }
    }
}
