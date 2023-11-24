using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatternConstructor.Models
{
    public class User: IdentityUser
    {
        [ForeignKey("Measure")]
        public int? MeasureId { get; set; }
        public Measure? Measure { get; set; }

        public ICollection<FavoriteArticle> FavoriteArticles { get; set; }
    }
}
