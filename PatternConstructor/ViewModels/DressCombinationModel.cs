using System.ComponentModel.DataAnnotations;

namespace PatternConstructor.ViewModels
{
    public class DressCombinationModel
    {
        [Display(Name = "Силуэт")]
        [Required]
        public string Silhouette { get; set; }

        [Display(Name = "Длина платья")]
        [Required]
        public string Length { get; set; }

        [Required]
        [Display(Name = "Горловина")]
        public string Neck { get; set; }

        [Required]
        [Display(Name = "Воротник")]
        public string Collar { get; set; }

        [Required]
        [Display(Name = "Застежка полочки")]
        public string Clasp { get; set; }

        [Required]
        [Display(Name = "Талия")]
        public string Waist { get; set; }

        [Required]
        [Display(Name = "Рукав")]
        public string Sleeve { get; set; }

        public bool DoubleContour { get; set; }

        public string[] Silhouettes = new[] { "Среднее", "Плотное", "Свободное" };
        public string[] Lengths = new[] { "Мини", "До колена", "Миди" };
        public string[] Necks = new[] { "Стандартная", "V-горловина", "Круглая" };
        public string[] Collars = new[] { "Без воротника", "Отложной с круглыми концами", "Отложной с прямыми углами", "Стойка с застежкой" };
        public string[] Clasps = new[] { "Без застежки", "Застежка на пуговицы до талии", "Застежка на пуговицы до низа", "Центральный шов полочки" };
        public string[] Waists = new[] { "Отрезнок по талии", "Неотрезное по талии"};
        public string[] Sleeves = new[] { "Без рукава", "Короткий", "Епископ с резинкой", "Епископ с манжетой" };
    }
}
