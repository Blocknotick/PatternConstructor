using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatternConstructor.Models
{
    public class FavoriteArticle
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public string? UserId { get; set; }
        public User? User { get; set; }

        [ForeignKey("Article")]
        public int? ArticleId { get; set; }
        public Article? Article { get; set; }
    }
}
