namespace PatternConstructor.Data.Enum
{
    public static class SkirtEnum
    {
        public static string[] Lengths = new[] { 
            "Мини", 
            "До колена", 
            "Миди" };
        public static string[] Types = new[] { 
            "Прямая", 
            "Тюльпан", 
            "Солнце", 
            "Полусолнце" };
        public static string[] Belts = new[] { 
            "Узкий", 
            "Средний", 
            "Широкий" };
        public static string[] Clasps = new[] { 
            "Пуговицы и молния", 
            "Потайная молния" };
        public static Dictionary<string, string> dict = new Dictionary<string, string>()
        {
            { Lengths[0], "https://www.sewist.com/files/130r130/snippets_images/077/e29/369.png" },
            { Lengths[1], "https://www.sewist.com/files/130r130/snippets_images/38d/b3a/367.png"},
            { Lengths[2], "https://www.sewist.com/files/130r130/snippets_images/03c/6b0/365.png"},
            { Types[0], "https://www.sewist.com/files/130r130/snippets_images/07c/580/832.png" },
            { Types[1], "https://www.sewist.com/files/130r130/snippets_images/c58/66e/2696.png"},
            { Types[2], "https://www.sewist.com/files/130r130/snippets_images/2af/e45/1688.png"},
            { Types[3], "https://www.sewist.com/files/130r130/snippets_images/5f2/c22/1682.png"},
            { Belts[0], "/skirtsimages/skirtbelt1.png"},
            { Belts[1], "/skirtsimages/skirtbelt2.png"},
            { Belts[2], "/skirtsimages/skirtbelt3.png"},
            { Clasps[0], "/skirtsimages/clasps2.png"},
            { Clasps[1], "/skirtsimages/clasps1.png"},
        };

        public static Dictionary<string, LengthType> lengthDict = new Dictionary<string, LengthType>()
        {
            {"Мини", LengthType.Mini},
            {"До колена", LengthType.Knee},
            {"Миди", LengthType.Midi},
        };

        public static Dictionary<string, SkirtType> skirtTypeDict = new Dictionary<string, SkirtType>()
        {
            {"Прямая", SkirtType.Pencil},
            {"Тюльпан", SkirtType.Tulip},
            {"Солнце", SkirtType.Sun},
            {"Полусолнце", SkirtType.HalfSun}
        };

        public static Dictionary<string, BeltType> beltTypeDict = new Dictionary<string, BeltType>()
        {
            {"Узкий", BeltType.Narrow},
            {"Средний", BeltType.Middle},
            {"Широкий", BeltType.Wode}
        };

        public static Dictionary<string, bool> claspDict = new Dictionary<string, bool>()
        {
            {"Пуговицы и молния", true},
            {"Потайная молния", false}
        };


    }
}
