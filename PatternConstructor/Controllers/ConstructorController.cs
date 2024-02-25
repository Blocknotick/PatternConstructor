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
using System.Text;
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

            Pattern skirtPattern = new Pattern();
            if (skirtConstructModel.SkirtCombinationModel.Type == "Солнце" || skirtConstructModel.SkirtCombinationModel.Type == "Полусолнце")
            {
                skirtPattern = new SunSkirtPattern(skirtConstructModel);
            }
            else
                skirtPattern = new PencilSkirt(skirtConstructModel);

            string documentContent = skirtPattern.GenerateContent();



            using (iText.Kernel.Pdf.PdfDocument doc =
                new iText.Kernel.Pdf.PdfDocument(new iText.Kernel.Pdf.PdfWriter(new FileStream("wwwroot/skirts/" + FileName, FileMode.OpenOrCreate),
                                                                                new WriterProperties().SetCompressionLevel(0))))
            {
                //doc.AddNewPage(PageSize.A4);
                doc.AddNewPage(new PageSize((float)(0.75*skirtPattern.widthcm),(float)(0.75*skirtPattern.heightcm)));
                //doc.AddNewPage(size);
                //ISvgConverterProperties properties = new SvgConverterProperties().SetBaseUri("wwwroot/skirts/temp.svg");
                //SvgConverter.DrawOnDocument(new FileStream("wwwroot/skirts/temp.svg", FileMode.Open, FileAccess.Read, FileShare.Read), doc, 1, properties);
                SvgConverter.DrawOnDocument(documentContent, doc, 1);

            }

            //using (iText.Kernel.Pdf.PdfDocument doc =
            //    new iText.Kernel.Pdf.PdfDocument(new iText.Kernel.Pdf.PdfWriter(new FileStream("wwwroot/skirts/" + FileName, FileMode.OpenOrCreate),
            //                                                                    new WriterProperties().SetCompressionLevel(0))))
            //{
            //    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(documentContent);
            //    float width = image.Width;
            //    float height = image.Height;


            //    PdfContentByte canvas;
            //    SvgConverter.DrawOnPage(documentContent, doc.AddNewPage(0, PageSize.A4), 100, 100);
            //    SvgConverter.DrawOnPage(documentContent, doc.AddNewPage(1, PageSize.A4), 200, 200);
            //    canvas.AddImage(image, width, 0, 0, height, 0, -height / 2);
            //    document.NewPage();
            //    canvas.AddImage(image, width, 0, 0, height, 0, 0);
            //    document.NewPage();
            //    canvas.AddImage(image, width, 0, 0, height, -width / 2, -height / 2);
            //    document.NewPage();
            //    canvas.AddImage(image, width, 0, 0, height, -width / 2, 0);

            //}
            

            //using (FileStream outputStream = new FileStream("wwwroot/skirts/" + FileName, FileMode.Create))
            //{
            //    iTextSharp.text.pdf.PdfDocument document = new iTextSharp.text.pdf.PdfDocument();
            //    //document.SetMargins(40, 40, 40, 40);

            //        iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, outputStream);
            //    writer.SetPageSize(new iTextSharp.text.Rectangle(595f,842f));
            //    writer.SetMargins(40, 40, 40, 40);

            //    document.AddWriter(writer);
            //        iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader("wwwroot/skirts/" + user.Id + createdFile.Id.ToString() + "big.pdf");

            //    reader.ConsolidateNamedDestinations(); // Assuming 'editedPageNo' is related to named destinations
            //        document.Open();
            //        PdfImportedPage page = writer.GetImportedPage(reader, 1);
            //        PdfContentByte cb = writer.DirectContent;
            //        cb.AddTemplate(page, 1.4f, 0, 0, 1.19f, -13, 7);
            //        document.Close();


            //}


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

            htmldocs.Add("wwwroot/DescriptionUnits/skirts/decatification.pdf");
            htmldocs.Add("wwwroot/DescriptionUnits/skirts/cutting.pdf");
            htmldocs.Add("wwwroot/DescriptionUnits/skirts/interfacing.pdf");
            if (skirtConstructModel.SkirtCombinationModel.Type == "Прямая" || skirtConstructModel.SkirtCombinationModel.Type == "Тюльпан")
            {
                htmldocs.Add("wwwroot/DescriptionUnits/skirts/darts.pdf");
                htmldocs.Add("wwwroot/DescriptionUnits/skirts/sideSeams.pdf");
            }
            if (skirtConstructModel.SkirtCombinationModel.Type == "Солнце")
                htmldocs.Add("wwwroot/DescriptionUnits/skirts/sideSeams.pdf");
            else htmldocs.Add("wwwroot/DescriptionUnits/skirts/middleSeam.pdf");
            
            if (skirtConstructModel.SkirtCombinationModel.Clasp == "Потайная молния")
                htmldocs.Add("wwwroot/DescriptionUnits/skirts/beltbeforezipper.pdf");
            else
                htmldocs.Add("wwwroot/DescriptionUnits/skirts/zipperbeforebelt.pdf");
            htmldocs.Add("wwwroot/DescriptionUnits/skirts/BottomSeam.pdf");

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

        [Authorize]
        public ActionResult DressCombination()
        {
            DressCombinationModel dressCombination = new DressCombinationModel();
            return View(dressCombination);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DressCombination(DressCombinationModel skirtCombination)
        {
            //проверка на реализуемость
            if (!ModelState.IsValid) return View(skirtCombination);

            return RedirectToAction("Dress", skirtCombination);
        }

        [Authorize]
        public async Task<IActionResult> DressAsync(DressCombinationModel skirtCombination)
        {
            var user = await _userManager.GetUserAsync(User);

            Measure measure;
            measure = _context.Measures.FirstOrDefault(m => m.MeasureId == user.MeasureId); ;

            DressConstructModel skirtConstructModel = new DressConstructModel
            {
                WaistGirth = measure.WaistGirth,
                BustGirth = measure.BustGirth,
                HipsGirth = measure.HipsGirth,
                NeckGirth = measure.NeckGirth,
                BustGirthUp = measure.BustGirthUp,
                BustGirthSecond = measure.BurstGirthSecond,
                BackWaistLength = measure.BackWaistLength,
                BustHeight = measure.BustHeight,
                ShoulderHeight = measure.ShoulderHeight,
                FrontWaistLength = measure.FrontWaistLength,
                BackWidth = measure.BackWidth,
                ShoulderToNeck = measure.ShoulderToNeck,
                BackArmholeDepth = measure.BackArmholeDepth,
                BustCenter = measure.BustCenter,
                ShoulderToWrist = measure.ShoulderToWrist,
                UpperArm = measure.UpperArm,
                BustWidth = measure.BustWidth,
                WaistFloorFrontLength = measure.WaistFloorFrontLength,
                DressCombinationModel = skirtCombination
            };

            var s_measures = _context.standartMeasures.ToList();
            ViewBag.Types = s_measures.Select(c => new SelectListItem { Text = "Размер " + c.Size + " Рост " + c.Height, Value = c.Id.ToString() }).ToList();

            return View(skirtConstructModel);
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DressAsync(DressConstructModel skirtConstructModel)
        {
            if (!ModelState.IsValid) return RedirectToAction("Dress", skirtConstructModel.DressCombinationModel);
            var user = await _userManager.GetUserAsync(User);
            var createdFile = new CreatedFile
            {
                UserId = user.Id,
            };
            await _context.AddAsync(createdFile);
            _context.SaveChanges();


            string FileName = user.Id + createdFile.Id.ToString() + ".pdf";


            Pattern skirtPattern = new DressPattern(skirtConstructModel);
            

            string documentContent = skirtPattern.GenerateContent();

            using (iText.Kernel.Pdf.PdfDocument doc =
                new iText.Kernel.Pdf.PdfDocument(new iText.Kernel.Pdf.PdfWriter(new FileStream("wwwroot/dresses/" + FileName, FileMode.OpenOrCreate),
                                                                                new WriterProperties().SetCompressionLevel(0))))
            {
                doc.AddNewPage(new PageSize((float)(0.75 * skirtPattern.widthcm), (float)(0.75 * skirtPattern.heightcm)));
                SvgConverter.DrawOnDocument(documentContent, doc, 1);

            }


            var htmldocs = new List<string>();

            htmldocs.Add("wwwroot/DescriptionUnits/skirts/decatification.pdf");
            htmldocs.Add("wwwroot/DescriptionUnits/skirts/cutting.pdf");
            htmldocs.Add("wwwroot/DescriptionUnits/skirts/interfacing.pdf");
            

            createPdf("wwwroot/descr/" + FileName, htmldocs);
            createdFile.PatternLink = "/dresses/" + FileName;
            createdFile.DescribtionLink = "/descr/" + FileName;
            createdFile.Name = skirtConstructModel.Name;
            _context.Update(createdFile);
            _context.SaveChanges();

            //переход к странице созданных файлов
            return RedirectToAction("History", "Account");
        }
    }
}
