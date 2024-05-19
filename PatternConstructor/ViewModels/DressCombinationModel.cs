using PatternConstructor.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace PatternConstructor.ViewModels
{
    public class DressCombinationModel
    {
        [Display(Name = "Силуэт")]
        [Required(ErrorMessage = "Поле обязательно")]
        public string Silhouette { get; set; }

        [Display(Name = "Длина платья")]
        [Required(ErrorMessage = "Поле обязательно")]
        public string Length { get; set; }

        [Required(ErrorMessage = "Поле обязательно")]
        [Display(Name = "Горловина")]
        public string Neck { get; set; }

        [Required(ErrorMessage = "Поле обязательно")]
        [Display(Name = "Воротник")]
        public string Collar { get; set; }

        [Required(ErrorMessage = "Поле обязательно")]
        [Display(Name = "Застежка полочки")]
        public string Clasp { get; set; }

        [Required(ErrorMessage = "Поле обязательно")]
        [Display(Name = "Талия")]
        public string Waist { get; set; }

        [Required(ErrorMessage = "Поле обязательно")]
        [Display(Name = "Рукав")]
        public string Sleeve { get; set; }

        [Display(Name = "Добавить второй контур")]
        public bool DoubleContour { get; set; }

    }
}
