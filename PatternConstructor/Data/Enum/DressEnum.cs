namespace PatternConstructor.Data.Enum
{
    public static class DressEnum
    {
        public static readonly string[] Silhouettes = new[] {
            "Среднее",
            "Плотное",
            "Свободное" 
        };
        public static readonly string[] Lengths = new[] {
            "Мини",
            "До колена",
            "Миди" 
        };
        public static readonly string[] Necks = new[] {
            "Стандартная",
            "V-горловина",
            "Круглая" 
        };
        public static readonly string[] Collars = new[] {
            "Без воротника",
            "Отложной с прямыми углами",
            "Стойка с застежкой" 
        };
        public static readonly string[] Clasps = new[] {
            "Без застежки",
            "Застежка на пуговицы до талии",
            "Застежка на пуговицы до низа",
            "Центральный шов полочки" 
        };
        static public readonly string[] Waists = new[] {
            "Отрезноe по талии",
            "Неотрезное по талии"
        };
        public static readonly string[] Sleeves = new[] {
            "Без рукава",
            "Короткий",
            "Епископ с резинкой",
            "Епископ с манжетой" 
        };
        public static readonly Dictionary<string, string> dict = new Dictionary<string, string>()
        {
            { Silhouettes[0], "https://www.sewist.com/files/130r130/snippets_images/a3c/65c/356.png" },
            { Silhouettes[1], "https://www.sewist.com/files/130r130/snippets_images/c81/e72/357.png"},
            { Silhouettes[2],"https://www.sewist.com/files/130r130/snippets_images/272/3d0/358.png" },
            { Lengths[0], "https://www.sewist.com/files/130r130/snippets_images/077/e29/369.png" },
            { Lengths[1], "https://www.sewist.com/files/130r130/snippets_images/38d/b3a/367.png"},
            { Lengths[2], "https://www.sewist.com/files/130r130/snippets_images/03c/6b0/365.png"},
            { Necks[0], "https://www.sewist.com/files/130r130/snippets_images/9b0/4d1/3071.png"},
            { Necks[1], "https://www.sewist.com/files/130r130/snippets_images/20f/075/373.png"},
            { Necks[2], "https://www.sewist.com/files/130r130/snippets_images/872/488/3075.png"},
            { Collars[0], "https://www.sewist.com/files/130r130/snippets_images/6da/900/2483.png"},
            { Collars[1], "https://www.sewist.com/files/130r130/snippets_images/b01/693/3961.png"},
            { Collars[2], "https://www.sewist.com/files/130r130/snippets_images/ab4/f2b/4073.png"},
            { Clasps[0], "https://www.sewist.com/files/130r130/snippets_images/205/0e0/592.png"},
            { Clasps[1], "https://www.sewist.com/files/130r130/snippets_images/5ef/0b4/595.png"},
            { Clasps[2], "https://www.sewist.com/files/130r130/snippets_images/959/a55/884.png"},
            { Clasps[3], "https://www.sewist.com/files/130r130/snippets_images/097/e26/3841.png"},
            { Waists[0], "https://www.sewist.com/files/130r130/snippets_images/288/cc0/891.png"},
            { Waists[1], "https://www.sewist.com/files/130r130/snippets_images/758/874/429.png" },
            { Sleeves[0], "https://www.sewist.com/files/130r130/snippets_images/46b/a9f/586.png" },
            { Sleeves[1], "https://www.sewist.com/files/130r130/snippets_images/839/ab4/403.png" },
            { Sleeves[2], "https://www.sewist.com/files/130r130/snippets_images/92f/de8/4677.png" },
            { Sleeves[3], "https://www.sewist.com/files/130r130/snippets_images/abd/eb6/4127.png" },
        };
        public static Dictionary<string, SilhouetteType> silhouetteDict = new Dictionary<string, SilhouetteType>()
        {
            {"Среднее", SilhouetteType.Middle},
            {"Плотное", SilhouetteType.Tight},
            {"Свободное", SilhouetteType.Wide},
        };
        public static Dictionary<string, LengthType> lengthDict = new Dictionary<string, LengthType>()
        {
            {"Мини", LengthType.Mini},
            {"До колена", LengthType.Knee},
            {"Миди", LengthType.Midi},
        };
        public static Dictionary<string, NeckType> neckDict = new Dictionary<string, NeckType>()
        {
            {"Стандартная", NeckType.Standard},
            {"V-горловина", NeckType.Vneck},
            {"Круглая", NeckType.Rounded},
        };
        public static Dictionary<string, CollarType> collarDict = new Dictionary<string, CollarType>()
        {
            {"Без воротника", CollarType.None},
            {"Отложной с прямыми углами", CollarType.PeterPan},
            {"Стойка с застежкой", CollarType.StandingWithClasp},
        };
        public static Dictionary<string, FrontClaspType> claspDict = new Dictionary<string, FrontClaspType>()
        {
            {"Без застежки", FrontClaspType.None},
            {"Застежка на пуговицы до талии", FrontClaspType.ButtonsWaist},
            {"Застежка на пуговицы до низа", FrontClaspType.ButtonsWhole},
            {"Центральный шов полочки", FrontClaspType.CentralSeam},
        };
        public static Dictionary<string, WaistType> waistDict = new Dictionary<string, WaistType>()
        {
            {"Неотрезное по талии", WaistType.Whole},
            {"Отрезноe по талии", WaistType.Separated}
        };
        public static Dictionary<string, SleeveType> sleeveDict = new Dictionary<string, SleeveType>()
        {
            {"Без рукава", SleeveType.None},
            {"Короткий", SleeveType.Short},
            {"Епископ с резинкой", SleeveType.BishopRibbon},
            {"Епископ с манжетой", SleeveType.BishopCuff},
        };
    }
}
