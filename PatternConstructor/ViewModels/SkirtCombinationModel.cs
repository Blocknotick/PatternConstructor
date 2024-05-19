using PatternConstructor.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace PatternConstructor.ViewModels
{
    public class SkirtCombinationModel
    {
        [Display(Name = "Длина юбки")]
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public string Length { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Тип юбки")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Ширина пояса")]
        public string Belt { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Застежка")]
        public string Clasp { get; set; }

        [Display(Name = "Двойные контуры")]
        public bool DoubleContour { get; set; }

        
    }
}
