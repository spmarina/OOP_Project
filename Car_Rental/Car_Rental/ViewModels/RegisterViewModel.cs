using System.ComponentModel.DataAnnotations;

namespace Car_Rental.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Логин")]
        public string CreateLogin { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        public string CreatePassword { get; set; }

        //[Required]
        //[Compare("Password", ErrorMessage = "Пароли не совпадают")]
        //[Display(Name = "Подтвердить пароль")]
        //public string PasswordConfirm { get; set; }
    }
}
