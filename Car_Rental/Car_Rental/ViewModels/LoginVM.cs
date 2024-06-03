using System.ComponentModel.DataAnnotations;
namespace Car_Rental.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Введите ваш Login")]
        [EmailAddress(ErrorMessage = "Неверный формат Login")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите ваш пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
