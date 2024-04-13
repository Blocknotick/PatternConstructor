using System.ComponentModel.DataAnnotations;

namespace PatternConstructor.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Поле e-mail обязательно"), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Поле пароль обязательно"), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
