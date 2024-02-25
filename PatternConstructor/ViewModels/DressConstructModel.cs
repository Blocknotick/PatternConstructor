using System.ComponentModel.DataAnnotations;

namespace PatternConstructor.ViewModels
{
    public class DressConstructModel
    {
        [Display(Name = "Обхват талии")]
        [Required]
        [Range(50, 130)]
        public double WaistGirth { get; set; }

        [Display(Name = "Обхват груди 3")]
        [Range(70, 130, ErrorMessage = "Число должно быть в диапазоне от 70 до 90")]
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public double BustGirth { get; set; }


        [Display(Name = "Обхват бедер")]
        [Range(70, 150, ErrorMessage = "Число должно быть в диапазоне от 70 до 150")]
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public double HipsGirth { get; set; }


        [Display(Name = "Обхват шеи")]
        [Range(15, 50, ErrorMessage = "Число должно быть в диапазоне от 15 до 25")]
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public double NeckGirth { get; set; }



        [Display(Name = "Обхват груди над грудью (Обхват груди 1)")]
        [Range(70, 130, ErrorMessage = "Число должно быть в диапазоне от 90 до 130")]
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public double BustGirthUp { get; set; }


        [Display(Name = "Обхват груди 2")]
        [Range(50, 150, ErrorMessage = "Число должно быть в диапазоне от 90 до 130")]
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public double BustGirthSecond { get; set; }


        [Display(Name = "Длина талии спинки")]
        [Range(20, 90)]
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public double BackWaistLength { get; set; }

        [Display(Name = "Высота груди")]
        [Range(10, 90)]
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public double BustHeight { get; set; }

        [Display(Name = "Высота плеча косая")]
        [Range(10, 100)]
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public double ShoulderHeight { get; set; }

        [Display(Name = "Длина талии переда")]
        [Range(10, 100)]
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public double FrontWaistLength { get; set; }

        [Display(Name = "Ширина спинки")]
        [Range(10, 100)]
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public double BackWidth { get; set; }

        [Display(Name = "Ширина(длина) плеча")]
        [Range(5, 20)]
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public double ShoulderToNeck { get; set; }

        [Display(Name = "Высота проймы сзади")]
        [Range(10, 100)]
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public double BackArmholeDepth { get; set; }

        [Display(Name = "Центр груди")]
        [Range(5, 40)]
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public double BustCenter { get; set; }

        [Display(Name = "Длина рукава")]
        [Range(10, 100)]
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public double ShoulderToWrist { get; set; }

        [Display(Name = "Обхват плеча")]
        [Range(10, 100)]
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public double UpperArm { get; set; }

        [Display(Name = "Ширина спинки")]
        [Range(10, 100)]
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public double BustWidth { get; set; }

        [Display(Name = "Расстояние от талии до пола спереди")]
        [Range(10, 100)]
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public double WaistFloorFrontLength { get; set; }


        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public string Name { get; set; }

        public DressCombinationModel DressCombinationModel { get; set; }
    }
}
