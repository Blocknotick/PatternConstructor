using System.ComponentModel.DataAnnotations;

namespace PatternConstructor.Models
{
    public class DescriptionUnit
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }

    }
}
