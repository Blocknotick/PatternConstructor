using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatternConstructor.Models
{
    public class StandartMeasure
    {
        [Key]
        public int Id { get; set; }
        public int Size { get; set; }
        public int Height { get; set; }

        [ForeignKey("Measure")]
        public int? MeasureId { get; set; }
        public Measure? Measure { get; set; }
    }
}
