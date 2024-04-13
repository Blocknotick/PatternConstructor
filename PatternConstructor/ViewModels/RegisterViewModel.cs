using System.ComponentModel.DataAnnotations;

namespace PatternConstructor.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Поле e-mail обязательно"), DataType(DataType.EmailAddress, ErrorMessage ="e-mail должен быть действительным")]
        
        public string Email { get; set; }

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Поле пароль обязательно")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Повторите пароль")]
        [Required(ErrorMessage = "Поле пароль обязательно")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Поля пароль и повторите пароль должны совпадать")]
        public string ConfirmPassword { get; set; }

    }
}
