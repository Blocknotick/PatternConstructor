//using iText.Kernel.Pdf;
using iTextSharp.text.pdf;
using iText.Html2pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PatternConstructor.Data;
using PatternConstructor.Models;
using PatternConstructor.ViewModels;
using iText.Kernel.Pdf;
using iText.Kernel.Geom;
using iText.Svg.Processors;
using iText.Svg.Processors.Impl;
using iText.Svg.Converter;
//using Aspose.Svg;
//using Aspose.Svg.Saving;
//using Aspose.Imaging;
////using Aspose.Svg.Rendering.Pdf;
////using Aspose.Svg.Rendering;

////using Aspose.Html;
////using Aspose.Html.Drawing;
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

            // Create an instance of SvgRenderer
            //SvgRenderer renderer = new SvgRenderer();

            //// Specify PdfRenderingOptions
            //var options = new PdfRenderingOptions()
            //{
            //    // Set Page Setup properties
            //    PageSetup =
            //    {
            //        Sizing = SizingType.FitContent
            //    }
            //};




            //var user = await _userManager.GetUserAsync(User);
            //var createdFile = new CreatedFile
            //{
            //    UserId = user.Id,
            //};
            //await _context.AddAsync(createdFile);
            //_context.SaveChanges();

            //string FileName = user.Id + createdFile.Id.ToString()+".pdf";

            //// Create an instance of PdfDevice
            //PdfDevice device = new PdfDevice(options, dataDir + FileName);

            //// Merge or combine all SVG documents to a PDF file.
            //renderer.Render(device, document, document);

            //createdFile.PatternLink = "/sunskirts/" + FileName;
            //createdFile.Name = sunSkirtConstructModel.Name;
            //_context.Update(createdFile);
            //_context.SaveChanges();

            //переход к странице созданных файлов
            return RedirectToAction("History", "Account");
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
            double belt=0;
            for (int i = 0; i< skirtConstructModel.SkirtCombinationModel.Belts.Length; i++)
            {
                if (skirtConstructModel.SkirtCombinationModel.Belt == skirtConstructModel.SkirtCombinationModel.Belts[i])
                    belt = i + 3;
            }
            string documentContent="";
            if (skirtConstructModel.SkirtCombinationModel.Type == "Солнце" || skirtConstructModel.SkirtCombinationModel.Type == "Полусолнце")
            {
                int deg = 180;
                if (skirtConstructModel.SkirtCombinationModel.Type == "Солнце")
                    deg = 360;
                SunSkirtPattern sunSkirtPattern = new(50, skirtConstructModel.WaistGirth, belt, false, deg);
                documentContent = sunSkirtPattern.GenerateContent();
            }
                
            //string documentContent = "<svg xmlns=\"http://www.w3.org/2000/svg\"><circle cx=\"50\" cy=\"50\" r=\"40\" /></svg>";
            // Initialize an object of SVGDocument class from the string content
            //Aspose.Svg.SVGDocument document = new Aspose.Svg.SVGDocument(documentContent, ".");


            using (iText.Kernel.Pdf.PdfDocument doc =
                new iText.Kernel.Pdf.PdfDocument(new iText.Kernel.Pdf.PdfWriter(new FileStream("wwwroot/skirts/" + FileName, FileMode.OpenOrCreate),
                                                                                new WriterProperties().SetCompressionLevel(0))))
            {
                doc.AddNewPage(PageSize.A4);
                //ISvgConverterProperties properties = new SvgConverterProperties().SetBaseUri(SVG_FILE);
                SvgConverter.DrawOnDocument(documentContent, doc, 1);

            }


            //ConvertPatternToPDF(new Aspose.Svg.SVGDocument[] {document, document}, dataDir + FileName);
            //логика генерации описания пошива

            var htmldocs = new List<string>();
            //if (skirtConstructModel.SkirtCombinationModel.Type == "Прямая" || skirtConstructModel.SkirtCombinationModel.Type == "Тюльпан")
            //    htmldocs.Add("wwwroot/DescriptionUnits/dart.html");
            //htmldocs.Add("wwwroot/DescriptionUnits/interfacing.html");
            //htmldocs.Add("wwwroot/DescriptionUnits/belt.html");

            //htmldocs.Add("wwwroot/DescriptionUnits/sideseam.html");
            //htmldocs.Add("wwwroot/DescriptionUnits/bottomseam.html");
            //if (skirtConstructModel.SkirtCombinationModel.Clasp == "Потайная молния")
            //    htmldocs.Add("wwwroot/DescriptionUnits/zipper.html");
            //else
            //    htmldocs.Add("wwwroot/DescriptionUnits/button.html");

            htmldocs.Add("wwwroot/DescriptionUnits/bottomseam.pdf");
            if (skirtConstructModel.SkirtCombinationModel.Clasp == "Потайная молния")
                htmldocs.Add("wwwroot/DescriptionUnits/zipper.pdf");
            else
                htmldocs.Add("wwwroot/DescriptionUnits/button.pdf");

            createPdf("wwwroot/descr/" + FileName, htmldocs);
            //GenerateDescription(htmldocs, "wwwroot/descr/" + FileName);
            createdFile.PatternLink = "/skirts/" + FileName;
            createdFile.DescribtionLink = "/descr/" + FileName;
            createdFile.Name = skirtConstructModel.Name;
            _context.Update(createdFile);
            _context.SaveChanges();

            //переход к странице созданных файлов
            return RedirectToAction("History","Account");
        }

        public void createPdf(string file, List<string> HTML){
            iTextSharp.text.Document document = new iTextSharp.text.Document();
            PdfCopy copy = new PdfCopy(document, new FileStream(file, FileMode.Create, FileAccess.Write));
            document.Open();
            iTextSharp.text.pdf.PdfReader reader;
            foreach (var html in HTML)
            {
                reader = new iTextSharp.text.pdf.PdfReader(html);
                copy.AddDocument(reader);
                reader.Close();
            }
            document.Close();

        }
        //void GenerateDescription(List<string> htmldocs, string outputpath)
        //{
        //    var doc = new List<Aspose.Html.HTMLDocument>();
        //    //Aspose.Html.HTMLDocument [] doc = new Aspose.Html.HTMLDocument[0];
        //    for (int i = 0; i < htmldocs.Count; i++)
        //    {
        //        doc.Add(new Aspose.Html.HTMLDocument(htmldocs[i]));
        //    }
        //    //var document1 = new Aspose.Html.HTMLDocument("wwwroot/test.html");

        //    //поля бы добавить
        //    var renderer1 = new Aspose.Html.Rendering.HtmlRenderer();
        //    //renderer1.Render(new Aspose.Html.Rendering.Pdf.PdfDevice(dataDir + "output.pdf"), document1, document1);
        //    //renderer1.Render(new Aspose.Html.Rendering.Pdf.PdfDevice(outputpath), document1, document1);
        //    //var options = new Aspose.Html.Rendering.Pdf.PdfRenderingOptions();
        //    //options.PageSetup.AnyPage = new Page(new Margin(10));
        //    renderer1.Render(new Aspose.Html.Rendering.Pdf.PdfDevice(outputpath),doc.ToArray());
        //}

        //void ConvertPatternToPDF(Aspose.Svg.SVGDocument[] svgfiles, string outputpath)
        //{
        //    // Create an instance of SvgRenderer
        //    SvgRenderer renderer = new SvgRenderer();

        //    // Specify PdfRenderingOptions
        //    var options = new PdfRenderingOptions();

        //    // Create an instance of PdfDevice
        //    PdfDevice device = new PdfDevice(options, outputpath);

        //    // Merge or combine all SVG documents to a PDF file.
        //    renderer.Render(device, svgfiles);

        //}
    }
}
