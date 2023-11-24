using System.ComponentModel.DataAnnotations;

namespace PatternConstructor.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "E-mail")]
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Пароль")]
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
