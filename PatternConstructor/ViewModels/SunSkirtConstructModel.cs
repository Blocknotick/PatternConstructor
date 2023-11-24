using System.ComponentModel.DataAnnotations;

namespace PatternConstructor.ViewModels
{
    public class SunSkirtConstructModel
    {
        [Required]
        [Range(50,130)]
        public double WaistGirth { get; set; }
        [Required]
        [Range(15, 130)]
        public double Length { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
