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

        public string[] Silhouettes = new[] { "Среднее", "Плотное", "Свободное" };
        public string[] Lengths = new[] { "Мини", "До колена", "Миди" };
        public string[] Necks = new[] { "Стандартная", "V-горловина", "Круглая" };
        public string[] Collars = new[] { "Без воротника", "Отложной с прямыми углами", "Стойка с застежкой" };
        public string[] Clasps = new[] { "Без застежки", "Застежка на пуговицы до талии", "Застежка на пуговицы до низа", "Центральный шов полочки" };
        public string[] Waists = new[] { "Отрезноe по талии", "Неотрезное по талии"};
        public string[] Sleeves = new[] { "Без рукава", "Короткий", "Епископ с резинкой", "Епископ с манжетой" };
        public Dictionary<string, string> dict = new Dictionary<string, string>()
        {
            { "Среднее", "https://www.sewist.com/files/130r130/snippets_images/a3c/65c/356.png" },
            { "Плотное", "https://www.sewist.com/files/130r130/snippets_images/c81/e72/357.png"},
            { "Свободное","https://www.sewist.com/files/130r130/snippets_images/272/3d0/358.png" },
            { "Мини", "https://www.sewist.com/files/130r130/snippets_images/077/e29/369.png" },
            { "До колена", "https://www.sewist.com/files/130r130/snippets_images/38d/b3a/367.png"},
            { "Миди", "https://www.sewist.com/files/130r130/snippets_images/03c/6b0/365.png"},
            { "Стандартная", "https://www.sewist.com/files/130r130/snippets_images/9b0/4d1/3071.png"},
            { "V-горловина", "https://www.sewist.com/files/130r130/snippets_images/20f/075/373.png"},
            { "Круглая", "https://www.sewist.com/files/130r130/snippets_images/872/488/3075.png"},
            { "Без воротника", "https://www.sewist.com/files/130r130/snippets_images/6da/900/2483.png"},
            { "Отложной с прямыми углами", "https://www.sewist.com/files/130r130/snippets_images/b01/693/3961.png"},
            { "Стойка с застежкой", "https://www.sewist.com/files/130r130/snippets_images/ab4/f2b/4073.png"},
            { "Без застежки", "https://www.sewist.com/files/130r130/snippets_images/205/0e0/592.png"},
            { "Застежка на пуговицы до талии", "https://www.sewist.com/files/130r130/snippets_images/5ef/0b4/595.png"},
            { "Застежка на пуговицы до низа", "https://www.sewist.com/files/130r130/snippets_images/959/a55/884.png"},
            { "Центральный шов полочки", "https://www.sewist.com/files/130r130/snippets_images/097/e26/3841.png"},
            { "Отрезноe по талии", "https://www.sewist.com/files/130r130/snippets_images/288/cc0/891.png"},
            { "Неотрезное по талии", "https://www.sewist.com/files/130r130/snippets_images/758/874/429.png" },
            { "Без рукава", "https://www.sewist.com/files/130r130/snippets_images/46b/a9f/586.png" },
            { "Короткий", "https://www.sewist.com/files/130r130/snippets_images/839/ab4/403.png" },
            { "Епископ с резинкой", "https://www.sewist.com/files/130r130/snippets_images/92f/de8/4677.png" },
            { "Епископ с манжетой", "https://www.sewist.com/files/130r130/snippets_images/abd/eb6/4127.png" },
        };
    }
}
