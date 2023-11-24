using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatternConstructor.Models
{
    public class CreatedFile
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }

        [ForeignKey("User")]
        public string? UserId { get; set; }
        public User? User { get; set; }

        public string? PatternLink { get; set; }
        public string? DescribtionLink { get; set; }
    }
}
