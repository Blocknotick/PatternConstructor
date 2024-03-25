﻿//using iText.Kernel.Pdf;
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

            var user = await _userManager.GetUserAsync(User);
            var createdFile = new CreatedFile
            {
                UserId = user.Id,
            };
            await _context.AddAsync(createdFile);
            _context.SaveChanges();

            string FileName = user.Id + createdFile.Id.ToString() + ".pdf";

            //Здесь должна быть функция, которая генерирует выкройку

            Pattern skirtPattern = new SunSkirtPattern(sunSkirtConstructModel.Length, sunSkirtConstructModel.WaistGirth, 0, false, sunSkirtConstructModel.Degree, false, sunSkirtConstructModel.WaistP);


            string documentContent = skirtPattern.GenerateContent();



            using (iText.Kernel.Pdf.PdfDocument doc =
                new iText.Kernel.Pdf.PdfDocument(new iText.Kernel.Pdf.PdfWriter(new FileStream("wwwroot/skirts/" + FileName, FileMode.OpenOrCreate),
                                                                                new WriterProperties().SetCompressionLevel(0))))
            {
                doc.AddNewPage(new PageSize((float)(0.75 * skirtPattern.widthcm), (float)(0.75 * skirtPattern.heightcm)));
                SvgConverter.DrawOnDocument(documentContent, doc, 1);

            }


            createdFile.PatternLink = "/skirts/" + FileName;
            createdFile.Name = sunSkirtConstructModel.Name;
            _context.Update(createdFile);
            _context.SaveChanges();

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
                doc.AddNewPage(new PageSize((float)(0.75*skirtPattern.widthcm),(float)(0.75*skirtPattern.heightcm)));
                SvgConverter.DrawOnDocument(documentContent, doc, 1);

            }


            var htmldocs = new List<string>();

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
            if (skirtCombination.Neck=="V-горловина"&&skirtCombination.Collar=="Стойка с застежкой" ||
                skirtCombination.Collar=="Стойка с застежкой"&&(skirtCombination.Clasp== "Без застежки"||skirtCombination.Clasp== "Центральный шов полочки") ||
                skirtCombination.Waist== "Неотрезное по талии"&&skirtCombination.Clasp== "Застежка на пуговицы до талии") return View(skirtCombination);

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
                WristGirth = measure.WristGirth,
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
