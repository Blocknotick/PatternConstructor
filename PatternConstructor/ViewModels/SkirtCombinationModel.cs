using PatternConstructor.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace PatternConstructor.ViewModels
{
    public class SkirtCombinationModel
    {
        [Display(Name = "Длина юбки")]
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public string Length { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Тип юбки")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Ширина пояса")]
        public string Belt { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Застежка")]
        public string Clasp { get; set; }

        [Display(Name = "Двойные контуры")]
        public bool DoubleContour { get; set; }

        public string[] Lengths = new[] { "Мини", "До колена", "Миди" };
        public string[] Types = new[] { "Прямая", "Тюльпан", "Солнце", "Полусолнце" };
        public string[] Belts = new[] { "Узкий", "Средний", "Широкий" };
        public string[] Clasps = new[] { "Пуговицы и молния", "Потайная молния"};
        public Dictionary<string, string> dict = new Dictionary<string, string>()
        {
            { "Мини", "https://www.sewist.com/files/130r130/snippets_images/077/e29/369.png" },
            { "До колена", "https://www.sewist.com/files/130r130/snippets_images/38d/b3a/367.png"},
            { "Миди", "https://www.sewist.com/files/130r130/snippets_images/03c/6b0/365.png"},
            { "Прямая", "https://www.sewist.com/files/130r130/snippets_images/07c/580/832.png" },
            { "Тюльпан", "https://www.sewist.com/files/130r130/snippets_images/c58/66e/2696.png"},
            { "Солнце", "https://www.sewist.com/files/130r130/snippets_images/2af/e45/1688.png"},
            { "Полусолнце", "https://www.sewist.com/files/130r130/snippets_images/5f2/c22/1682.png"},
            { "Узкий", "/skirtsimages/skirtbelt1.png"},
            { "Средний", "/skirtsimages/skirtbelt2.png"},
            { "Широкий", "/skirtsimages/skirtbelt3.png"},
            { "Пуговицы и молния", "/skirtsimages/clasps2.png"},
            { "Потайная молния", "/skirtsimages/clasps1.png"},
        };
    }
}
