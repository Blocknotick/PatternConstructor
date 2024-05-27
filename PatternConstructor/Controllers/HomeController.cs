using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PatternConstructor.Data;
using PatternConstructor.Models;
using System.Diagnostics;

namespace PatternConstructor.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SizeCharts()
        {
            var s_measures = _context.standartMeasures.ToList();
            var sm = s_measures.First();
            sm.Measure = _context.Measures.FirstOrDefault(m => m.MeasureId == sm.MeasureId);
            ViewBag.Types = s_measures.OrderBy(x=>x.Height).OrderBy(x=>x.Size).Select(c => new SelectListItem { Text = "Размер "+ c.Size+" Рост "+c.Height, Value = c.Id.ToString() }).ToList();
            return View(sm);
        }

        public IActionResult ShowSizeTable(string type)
        {
            var s_measures = _context.standartMeasures.ToList();

            StandartMeasure sm = s_measures.First();

            if (!string.IsNullOrEmpty(type))
                sm = s_measures.Where(c => c.Id.ToString() == type).First();
            
            sm.Measure = _context.Measures.FirstOrDefault(m => m.MeasureId == sm.MeasureId);
            ViewBag.Types = s_measures.Select(c => new SelectListItem { Text = "Размер " + c.Size + " Рост " + c.Height, Value = c.Id.ToString() }).Distinct().ToList();
            return PartialView("_ShowTablePartialView", sm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}