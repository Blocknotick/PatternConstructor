using System.ComponentModel.DataAnnotations;

namespace PatternConstructor.ViewModels
{
    public class SunSkirtConstructModel
    {
        [Required]
        [Display(Name = "Обхват талии")]
        [Range(50,130)]
        public double WaistGirth { get; set; }
        [Required]
        [Display(Name = "Прибавка по талии (полная)")]
        [Range(0, 100)]
        public double WaistP { get; set; }
        [Required]
        [Display(Name = "Длина юбки")]
        [Range(15, 130)]
        public double Length { get; set; }
        [Required]
        [Display(Name = "Градусы (180 - полусолнце, 360 - солнце. Минимум = 90, Максимум = 720")]
        [Range(89, 721)]
        public int Degree { get; set; }

        [Display(Name = "Название выкройки")]
        [Required]
        public string Name { get; set; }
    }
}
