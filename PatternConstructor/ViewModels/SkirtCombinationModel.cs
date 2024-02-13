using PatternConstructor.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace PatternConstructor.ViewModels
{
    public class SkirtCombinationModel
    {
        [Display(Name = "Длина юбки")]
        [Required]
        public string Length { get; set; }
        [Required]
        [Display(Name = "Тип юбки")]
        public string Type { get; set; }
        [Required]
        [Display(Name = "Ширина пояса")]
        public string Belt { get; set; }
        [Required]
        [Display(Name = "Застежка")]
        public string Clasp { get; set; }
        public bool DoubleContour { get; set; }

        public string[] Lengths = new[] { "Мини", "До колена", "Миди" };
        public string[] Types = new[] { "Прямая", "Тюльпан", "Солнце", "Полусолнце" };
        public string[] Belts = new[] { "Узкий", "Средний", "Широкий" };
        public string[] Clasps = new[] { "Пуговицы и молния", "Потайная молния"};
    }
}
