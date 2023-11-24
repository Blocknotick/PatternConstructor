using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PatternConstructor.Data;
using PatternConstructor.Models;
using PatternConstructor.ViewModels;

namespace PatternConstructor.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public ArticlesController(UserManager<User> userManager, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var articles = _context.Articles.ToList();
            return View(articles);
        }

        public async Task<IActionResult> DetailArticleAsync(int id)
        {
            var article = _context.Articles.FirstOrDefault(x => x.Id == id);
            if (article == null) return NotFound();

            ArticleViewModel articleViewModel = new ArticleViewModel
            {
                article = article,
                isFavorite = false
            };

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser != null)
            {
                var f = _context.FavoriteArticles.FirstOrDefault(d => d.ArticleId == id && d.UserId == currentUser.Id);
                if (f != null)
                    articleViewModel.isFavorite = true;
            }
            return View(articleViewModel);
        }
    }
}
