using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PatternConstructor.Models;

namespace PatternConstructor.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<StandartMeasure> standartMeasures { get; set; }
        public DbSet<Measure> Measures { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<FavoriteArticle> FavoriteArticles { get; set; }
        public DbSet<CreatedFile> CreatedFiles { get; set; }
        //public DbSet<DescriptionUnit> DescriptionUnits { get; set; }

       
    }
}
