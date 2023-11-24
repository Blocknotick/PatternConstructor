using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PatternConstructor.Data;
using PatternConstructor.Models;
using PatternConstructor.ViewModels;
//using Aspose.Svg;
//using Aspose.Svg.Saving;
//using Aspose.Imaging;
using Aspose.Svg.Rendering.Pdf;
using Aspose.Svg.Rendering;

//using Aspose.Html;
//using Aspose.Html.Dom.Svg;
//using Aspose.Html.Converters;
//using Aspose.Html.Saving;

namespace PatternConstructor.Controllers
{
    public class ConstructorController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public ConstructorController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> BasicSunAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            Measure measure;
            measure = _context.Measures.FirstOrDefault(m => m.MeasureId == user.MeasureId); ;

            SunSkirtConstructModel sunSkirtConstructModel = new SunSkirtConstructModel
            {
                WaistGirth = measure.WaistGirth
            };


            var s_measures = _context.standartMeasures.ToList();
            ViewBag.Types = s_measures.Select(c => new SelectListItem { Text = "Размер " + c.Size + " Рост " + c.Height, Value = c.Id.ToString() }).ToList();

            return View(sunSkirtConstructModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> BasicSunAsync(SunSkirtConstructModel sunSkirtConstructModel)
        {
            if (!ModelState.IsValid) return RedirectToAction("BasicSun");
            //создание файлов выкройки
            string documentContent = "<svg xmlns=\"http://www.w3.org/2000/svg\"><circle cx=\"50\" cy=\"50\" r=\"40\" /></svg>";

            // Initialize an object of SVGDocument class from the string content
            Aspose.Svg.SVGDocument document = new Aspose.Svg.SVGDocument(documentContent, ".");

            
            
            // Save the document to a file
            //document.Save("wwwroot/sunskirts/test.svg", SVGSaveFormat.SVG);
            //document.Save("wwwroot/sunskirts/test1.svg", SVGSaveFormat.SVG);
            ////конвертировать в pdf
            ////Create an instance of PdfOptions
            //var image = Aspose.Imaging.Image.Load("wwwroot/sunskirts");
            //var exportOptions = new Aspose.Imaging.ImageOptions.PdfOptions();
            //Aspose.Imaging.ImageOptions.VectorRasterizationOptions rasterizationOptions = new Aspose.Imaging.ImageOptions.SvgRasterizationOptions();
            //rasterizationOptions.PageWidth = image.Width;
            //rasterizationOptions.PageHeight = image.Height;
            //exportOptions.VectorRasterizationOptions = rasterizationOptions;

            //// Save svg to pdf
            //image.Save(Path.Combine("wwwroot/sunskirts/", "output.pdf"), exportOptions);

            //File.Delete(Path.Combine(templatesFolder, "output.pdf"));


            //var options = new Aspose.Html.Saving.PdfSaveOptions();

            //Converter.ConvertSVG(document, options, "wwwroot/sunskirts/output.pdf");
            

            string dataDir = "wwwroot/sunskirts/";
            //SVGDocument document1 = new SVGDocument(dataDir + "test.svg");
            //SVGDocument document2 = new SVGDocument(dataDir + "test1.svg");
            //SVGDocument document3 = new SVGDocument(dataDir + "Lineto.svg");

            // Create an instance of SvgRenderer
            SvgRenderer renderer = new SvgRenderer();

            // Specify PdfRenderingOptions
            var options = new PdfRenderingOptions()
            {
                // Set Page Setup properties
                PageSetup =
                {
                    Sizing = SizingType.FitContent
                }
            };




            var user = await _userManager.GetUserAsync(User);
            var createdFile = new CreatedFile
            {
                UserId = user.Id,
            };
            await _context.AddAsync(createdFile);
            _context.SaveChanges();

            string FileName = user.Id + createdFile.Id.ToString()+".pdf";

            // Create an instance of PdfDevice
            PdfDevice device = new PdfDevice(options, dataDir + FileName);

            // Merge or combine all SVG documents to a PDF file.
            renderer.Render(device, document, document);

            createdFile.PatternLink = "/sunskirts/" + FileName;
            createdFile.Name = sunSkirtConstructModel.Name;
            _context.Update(createdFile);
            _context.SaveChanges();

            //переход к странице созданных файлов
            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<ActionResult> BasicDress()
        {
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult SkirtCombination()
        {
            SkirtCombinationModel skirtCombination = new SkirtCombinationModel();
            return View(skirtCombination);
        }

        [HttpPost]
        [Authorize]
        public ActionResult SkirtCombination(SkirtCombinationModel skirtCombination)
        {
            if (!ModelState.IsValid) return View(skirtCombination);

            return RedirectToAction("Skirt", skirtCombination);
        }

        [Authorize]
        public async Task<IActionResult> SkirtAsync(SkirtCombinationModel skirtCombination)
        {
            var user = await _userManager.GetUserAsync(User);

            Measure measure;
            measure = _context.Measures.FirstOrDefault(m => m.MeasureId == user.MeasureId); ;

            SkirtConstructModel skirtConstructModel = new SkirtConstructModel
            {
                WaistGirth = measure.WaistGirth,
                LegLength = measure.LegLength,
                HipsGirth = measure.HipsGirth,
                HipHeight = measure.HipHeight,
                WaistFloorSideLength = measure.WaistFloorSideLength,
                WaistFloorBackLength = measure.WaistFloorBackLength,
                WaistFloorFrontLength = measure.WaistFloorFrontLength,
                SkirtCombinationModel = skirtCombination 
            };

            var s_measures = _context.standartMeasures.ToList();
            ViewBag.Types = s_measures.Select(c => new SelectListItem { Text = "Размер " + c.Size + " Рост " + c.Height, Value = c.Id.ToString() }).ToList();

            return View(skirtConstructModel);
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SkirtAsync(SkirtConstructModel skirtConstructModel)
        {
            if (!ModelState.IsValid) return RedirectToAction("Skirt", skirtConstructModel.SkirtCombinationModel);
            var user = await _userManager.GetUserAsync(User);
            var createdFile = new CreatedFile
            {
                UserId = user.Id,
            };
            await _context.AddAsync(createdFile);
            _context.SaveChanges();

            string dataDir = "wwwroot/skirts/";
            string FileName = user.Id + createdFile.Id.ToString() + ".pdf";

            //Здесь должна быть функция, которая генерирует выкройку
            string documentContent = "<svg xmlns=\"http://www.w3.org/2000/svg\"><circle cx=\"50\" cy=\"50\" r=\"40\" /></svg>";
            // Initialize an object of SVGDocument class from the string content
            Aspose.Svg.SVGDocument document = new Aspose.Svg.SVGDocument(documentContent, ".");

            ConvertPatternToPDF(new Aspose.Svg.SVGDocument[] {document, document}, dataDir + FileName);
            GenerateDescription(new string[] { "wwwroot/test.html", "wwwroot/test.html" }, "wwwroot/descr/" + FileName);
            createdFile.PatternLink = "/skirts/" + FileName;
            createdFile.DescribtionLink = "/descr/" + FileName;
            createdFile.Name = skirtConstructModel.Name;
            _context.Update(createdFile);
            _context.SaveChanges();

            //переход к странице созданных файлов
            return RedirectToAction("History","Account");
        }


        void GenerateDescription(string[] htmldocs, string outputpath)
        {
            //replace to a for-cycle
            var doc = new List<Aspose.Html.HTMLDocument>();
            for (int i = 0; i < htmldocs.Length; i++)
            {
                doc.Add(new Aspose.Html.HTMLDocument(htmldocs[i]));
            }
            //var document1 = new Aspose.Html.HTMLDocument("wwwroot/test.html");


            var renderer1 = new Aspose.Html.Rendering.HtmlRenderer();
            //renderer1.Render(new Aspose.Html.Rendering.Pdf.PdfDevice(dataDir + "output.pdf"), document1, document1);
            //renderer1.Render(new Aspose.Html.Rendering.Pdf.PdfDevice(outputpath), document1, document1);
            renderer1.Render(new Aspose.Html.Rendering.Pdf.PdfDevice(outputpath),doc.ToArray());
        }

        void ConvertPatternToPDF(Aspose.Svg.SVGDocument[] svgfiles, string outputpath)
        {
            

            // Create an instance of SvgRenderer
            SvgRenderer renderer = new SvgRenderer();

            // Specify PdfRenderingOptions
            var options = new PdfRenderingOptions();

            

            // Create an instance of PdfDevice
            PdfDevice device = new PdfDevice(options, outputpath);

            // Merge or combine all SVG documents to a PDF file.
            renderer.Render(device, svgfiles);

        }
    }
}
