using System.ComponentModel.DataAnnotations;

namespace Car_Rental.DtoModels.Login
{
    public class LoginModel
    {
        [Required]
        public string? CreateLogin { get; init; }
    }
}
