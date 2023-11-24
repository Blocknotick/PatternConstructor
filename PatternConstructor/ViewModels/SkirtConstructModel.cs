using System.ComponentModel.DataAnnotations;

namespace PatternConstructor.ViewModels
{
    public class SkirtConstructModel
    {
        [Display(Name = "Обхват талии")]
        [Required]
        [Range(50, 130)]
        public double WaistGirth { get; set; }

        [Display(Name = "Длина ноги")]
        [Range(70, 90, ErrorMessage ="Число должно быть в диапазоне от 70 до 90")]
        [Required(ErrorMessage ="Поле должно быть заполнено")]
        public double LegLength { get; set; }


        [Display(Name = "Обхват бедер")]
        [Range(70, 150, ErrorMessage = "Число должно быть в диапазоне от 70 до 150")]
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public double HipsGirth { get; set; }


        [Display(Name = "Высота бедер")]
        [Range(15, 25, ErrorMessage = "Число должно быть в диапазоне от 15 до 25")]
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public double HipHeight { get; set; }



        [Display(Name = "Длина от талии до пола сбоку")]
        [Range(90, 130, ErrorMessage = "Число должно быть в диапазоне от 90 до 130")]
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public double WaistFloorSideLength { get; set; }


        [Display(Name = "Длина от талии до пола спереди")]
        [Range(90, 130, ErrorMessage = "Число должно быть в диапазоне от 90 до 130")]
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public double WaistFloorFrontLength { get; set; }


        [Display(Name = "Длина от талии до пола сзади")]
        [Range(90, 130, ErrorMessage = "Число должно быть в диапазоне от 90 до 130")]
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public double WaistFloorBackLength { get; set; }


        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public string Name { get; set; }

        public SkirtCombinationModel SkirtCombinationModel { get; set; }
    }
}
