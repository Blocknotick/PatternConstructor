using System.ComponentModel.DataAnnotations;

namespace PatternConstructor.Models
{
    public class Measure
    {
        [Key]
        public int MeasureId { get; set; }
        public double BustCenter { get; set; } // центр груди 17-25
        public double LegLength { get; set; } // длина ноги 71-89
        public double ShoulderToWrist { get; set; } // длина рукава 50-65
        public double SeatHeight { get; set; } //высота сидения 23-31
        public double HipsGirth { get; set; } //обхват бедер 78-134
        public double BustGirth { get; set; } //обхват груди 72-128
        public double UpperArm { get; set; } // обхват плеча 21-42
        public double WaistGirth { get; set; } // обхват талии 52-113
        public double WristGirth { get; set; } //обхват запястья 14-19
        public double HipHeight { get; set; } //высота бедер 18-23
        public double BustHeight { get; set; } //высота груди 22-37
        public double ShoulderToNeck { get; set; } // длина плеча 12-15
        public double ElbowLength { get; set; } //длина рукава до локтя 30-38
        public double BackWaistLength { get; set; } // длина талии спинки 37-45
        public double FrontWaistLength { get; set; } // длина талии переда 38-54
        public double BackArmholeDepth { get; set; } // глубина проймы спинки 17-45
        public double NeckGirth { get; set; } // обхват шеи 31-43
        public double BustWidth { get; set; } // ширина груди 28-43
        public double BottomWidth { get; set; } // ширина низа 36-64
        public double BustGirthUp { get; set; } // обхват груди над грудью 71-117
        public double SleeveWidthBottom { get; set; } // Ширина рукава внизу 19-25
        public double BackWidth { get; set; } // ширина спины 30-44
        public double WaistFloorSideLength { get; set; } // Длина от талии до пола сбоку 96-121
        public double WaistFloorFrontLength { get; set; } // Длина от талии до пола спереди 95-119
        public double WaistFloorBackLength { get; set; } // Длина от талии до пола сзади 95-120
    }
}
