using System.ComponentModel.DataAnnotations;

namespace PatternConstructor.ViewModels
{
    public class EditProfileViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Центр груди")]
        [Range(16, 30)]
        [Required]
        public double BustCenter { get; set; }

        [Display(Name = "Длина ноги")]
        [Range(70, 90)]
        [Required]
        public double LegLength { get; set; }

        [Display(Name = "Длина рукава")]
        [Range(45, 70)]
        [Required]
        public double ShoulderToWrist { get; set; }

        [Display(Name = "Высота сидения")]
        [Range(20, 35)]
        [Required]
        public double SeatHeight { get; set; }

        [Display(Name = "Обхват бедер")]
        [Range(70, 150)]
        [Required]
        public double HipsGirth { get; set; }

        [Display(Name = "Обхват груди")]
        [Range(70, 130)]
        [Required]
        public double BustGirth { get; set; }

        [Display(Name = "Обхват плеча")]
        [Range(20, 45)]
        [Required]
        public double UpperArm { get; set; }

        [Display(Name = "Обхват талии")]
        [Range(50, 120)]
        [Required]
        public double WaistGirth { get; set; }

        [Display(Name = "Обхват запястья")]
        [Range(12, 20)]
        [Required]
        public double WristGirth { get; set; }

        [Display(Name = "Высота бедер")]
        [Range(15, 25)]
        [Required]
        public double HipHeight { get; set; }

        [Display(Name = "Высота груди")]
        [Range(20, 40)]
        [Required]
        public double BustHeight { get; set; }

        [Display(Name = "Длина плеча")]
        [Range(10, 20)]
        [Required]
        public double ShoulderToNeck { get; set; }

        [Display(Name = "Длина рукава до локтя")]
        [Range(25, 40)]
        [Required]
        public double ElbowLength { get; set; }

        [Display(Name = "Длина талии спинки")]
        [Range(35, 50)]
        [Required]
        public double BackWaistLength { get; set; }

        [Display(Name = "Длина талии переда")]
        [Range(35, 60)]
        [Required]
        public double FrontWaistLength { get; set; }

        [Display(Name = "Глубина проймы спинки")]
        [Range(15, 45)]
        [Required]
        public double BackArmholeDepth { get; set; }

        [Display(Name = "Обхват шеи")]
        [Range(25, 50)]
        [Required]
        public double NeckGirth { get; set; }

        [Display(Name = "Ширина груди")]
        [Range(25, 50)]
        [Required]
        public double BustWidth { get; set; }

        [Display(Name = "Ширина низа")]
        [Range(30, 65)]
        [Required]
        public double BottomWidth { get; set; }

        [Display(Name = "Обхват груди над грудью")]
        [Range(70, 120)]
        [Required]
        public double BustGirthUp { get; set; }

        [Display(Name = "Ширина рукава внизу")]
        [Range(15, 30)]
        [Required]
        public double SleeveWidthBottom { get; set; }

        [Display(Name = "Ширина спинки")]
        [Range(25, 50)]
        [Required]
        public double BackWidth { get; set; }

        [Display(Name = "Длина от талии до пола сбоку")]
        [Range(90, 130)]
        [Required]
        public double WaistFloorSideLength { get; set; }

        [Display(Name = "Длина от талии до пола спереди")]
        [Range(90, 130)]
        [Required]
        public double WaistFloorFrontLength { get; set; }

        [Display(Name = "Длина от талии до пола сзади")]
        [Range(90, 130)]
        [Required]
        public double WaistFloorBackLength { get; set; }
    }
}
