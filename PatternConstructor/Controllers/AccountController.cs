using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PatternConstructor.Data;
using PatternConstructor.Models;
using PatternConstructor.ViewModels;

namespace PatternConstructor.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) return View(registerViewModel);

            var user = await _userManager.FindByEmailAsync(registerViewModel.Email);
            if (user != null)
            {
                TempData["Error"] = "Пользователь с таким email уже существует";
                return View(registerViewModel);
            }

            var newUser = new User()
            {
                Email = registerViewModel.Email,
                UserName = registerViewModel.Email,
                Measure = new Measure()
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);

            
            if (!newUserResponse.Succeeded)
            {
                TempData["Error"] = newUserResponse.Errors.First().Description;
                return View(registerViewModel);
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }
            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                TempData["Error"] = "Пароль неверный";
                return View(loginViewModel);
            }
            TempData["Error"] = "Пользователя с таким email не существует";
            return View(loginViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null) return RedirectToAction("Login");

            Measure measure;
            if (user.MeasureId == null)
            {
                measure = new Measure();
            }
            else
            {
                measure = _context.Measures.FirstOrDefault(m => m.MeasureId == user.MeasureId); ;
            }

            var userVM = new EditProfileViewModel
            {
                Id = user.Id,
                BackArmholeDepth = measure.BackArmholeDepth,
                BackWaistLength = measure.BackWaistLength,
                BackWidth = measure.BackWidth,
                BottomWidth = measure.BottomWidth,
                BustCenter = measure.BustCenter,
                BustGirth = measure.BustGirth,
                BustGirthUp = measure.BustGirthUp,
                BustHeight = measure.BustHeight,
                BustWidth = measure.BustWidth,
                ElbowLength = measure.ElbowLength,
                FrontWaistLength = measure.FrontWaistLength,
                HipHeight = measure.HipHeight,
                HipsGirth = measure.HipsGirth,
                LegLength = measure.LegLength,
                NeckGirth = measure.NeckGirth,
                SeatHeight = measure.SeatHeight,
                ShoulderToNeck = measure.ShoulderToNeck,
                ShoulderToWrist = measure.ShoulderToWrist,
                SleeveWidthBottom = measure.SleeveWidthBottom,
                UpperArm = measure.UpperArm,
                WaistFloorBackLength = measure.WaistFloorBackLength,
                WaistFloorFrontLength = measure.WaistFloorFrontLength,
                WaistFloorSideLength = measure.WaistFloorSideLength,
                WaistGirth = measure.WaistGirth,
                WristGirth = measure.WristGirth,
            };

            var s_measures = _context.standartMeasures.ToList();
            ViewBag.Types = s_measures.Select(c => new SelectListItem { Text = "Размер " + c.Size + " Рост " + c.Height, Value = c.Id.ToString() }).ToList();

            return View(userVM);
        }

        [HttpPost]
        public async Task<IActionResult> ChooseSizeAsync(string type, string id)
        {
            var sm = _context.standartMeasures.ToList().Where(c => c.Id.ToString() == type).First();
            sm.Measure = _context.Measures.FirstOrDefault(m => m.MeasureId == sm.MeasureId);


            var s_measures = _context.standartMeasures.ToList();

            ViewBag.Types = s_measures.Select(c => new SelectListItem { Text = "Размер " + c.Size + " Рост " + c.Height, Value = c.Id.ToString() }).Distinct().ToList();


            var user = await _userManager.FindByIdAsync(id);

            user.Measure = _context.Measures.FirstOrDefault(m => m.MeasureId == user.MeasureId);
            {
                user.Measure.BackArmholeDepth = sm.Measure.BackArmholeDepth;
                user.Measure.BackWaistLength = sm.Measure.BackWaistLength;
                user.Measure.BackWidth = sm.Measure.BackWidth;
                user.Measure.BottomWidth = sm.Measure.BottomWidth;

                user.Measure.BustCenter = sm.Measure.BustCenter;
                user.Measure.BustGirth = sm.Measure.BustGirth;
                user.Measure.BustGirthUp = sm.Measure.BustGirthUp;
                user.Measure.BustHeight = sm.Measure.BustHeight;

                user.Measure.BustWidth = sm.Measure.BustWidth;
                user.Measure.ElbowLength = sm.Measure.ElbowLength;
                user.Measure.FrontWaistLength = sm.Measure.FrontWaistLength;
                user.Measure.HipHeight = sm.Measure.HipHeight;

                user.Measure.HipsGirth = sm.Measure.HipsGirth;
                user.Measure.LegLength = sm.Measure.LegLength;
                user.Measure.NeckGirth = sm.Measure.NeckGirth;
                user.Measure.SeatHeight = sm.Measure.SeatHeight;
                user.Measure.ShoulderToNeck = sm.Measure.ShoulderToNeck;

                user.Measure.ShoulderToWrist = sm.Measure.ShoulderToWrist;
                user.Measure.SleeveWidthBottom = sm.Measure.SleeveWidthBottom;
                user.Measure.UpperArm = sm.Measure.UpperArm;
                user.Measure.WaistFloorBackLength = sm.Measure.WaistFloorBackLength;

                user.Measure.WaistFloorFrontLength = sm.Measure.WaistFloorFrontLength;
                user.Measure.WaistFloorSideLength = sm.Measure.WaistFloorSideLength;
                user.Measure.WaistGirth = sm.Measure.WaistGirth;
                user.Measure.WristGirth = sm.Measure.WristGirth;
            }


            var newUserResponse2 = await _userManager.UpdateAsync(user);

            return View("Profile");
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditProfileViewModel editProfileVM)
        {
            if (!ModelState.IsValid) return RedirectToAction("Profile");

            var user = await _userManager.FindByIdAsync(editProfileVM.Id);
            if (user.MeasureId == null)
                user.Measure = new Measure();
            else
            {
                user.Measure = _context.Measures.FirstOrDefault(m => m.MeasureId == user.MeasureId); ;
            }

            user.Measure.BackArmholeDepth = editProfileVM.BackArmholeDepth;
            user.Measure.BackWaistLength = editProfileVM.BackWaistLength;
            user.Measure.BackWidth = editProfileVM.BackWidth;
            user.Measure.BottomWidth = editProfileVM.BottomWidth;

            user.Measure.BustCenter = editProfileVM.BustCenter;
            user.Measure.BustGirth = editProfileVM.BustGirth;
            user.Measure.BustGirthUp = editProfileVM.BustGirthUp;
            user.Measure.BustHeight = editProfileVM.BustHeight;

            user.Measure.BustWidth = editProfileVM.BustWidth;
            user.Measure.ElbowLength = editProfileVM.ElbowLength;
            user.Measure.FrontWaistLength = editProfileVM.FrontWaistLength;
            user.Measure.HipHeight = editProfileVM.HipHeight;

            user.Measure.HipsGirth = editProfileVM.HipsGirth;
            user.Measure.LegLength = editProfileVM.LegLength;
            user.Measure.NeckGirth = editProfileVM.NeckGirth;
            user.Measure.SeatHeight = editProfileVM.SeatHeight;
            user.Measure.ShoulderToNeck = editProfileVM.ShoulderToNeck;

            user.Measure.ShoulderToWrist = editProfileVM.ShoulderToWrist;
            user.Measure.SleeveWidthBottom = editProfileVM.SleeveWidthBottom;
            user.Measure.UpperArm = editProfileVM.UpperArm;
            user.Measure.WaistFloorBackLength = editProfileVM.WaistFloorBackLength;

            user.Measure.WaistFloorFrontLength = editProfileVM.WaistFloorFrontLength;
            user.Measure.WaistFloorSideLength = editProfileVM.WaistFloorSideLength;
            user.Measure.WaistGirth = editProfileVM.WaistGirth;
            user.Measure.WristGirth = editProfileVM.WristGirth;


            var newUserResponse = await _userManager.UpdateAsync(user);
            if (!newUserResponse.Succeeded)
            {
                //обновление юзера не случилось
                return View(editProfileVM);
            }

            return RedirectToAction("Profile", "Account");
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Favorites()
        {
            var user = await _userManager.GetUserAsync(User);

            user.FavoriteArticles = _context.FavoriteArticles.
                Where(fd => fd.UserId == user.Id).
                Include(d => d.Article).ToList();

            return View(user);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteFavorite(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var favoriteArticle = _context.FavoriteArticles.
                Where(fd => fd.ArticleId == id && fd.UserId == user.Id).First();
            _context.Remove(favoriteArticle);
            _context.SaveChanges();
            return RedirectToAction("Favorites");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddFavorite(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var favoriteArticle = new FavoriteArticle
            {
                UserId = user.Id,
                ArticleId = id
            };
            _context.AddAsync(favoriteArticle);
            _context.SaveChanges();
            return RedirectToAction("Favorites");
        }


        [Authorize]
        public async Task<IActionResult> History()
        {
            var user = await _userManager.GetUserAsync(User);

            var createdFiles = _context.CreatedFiles.Where(m => m.UserId == user.Id).ToList();
            
            return View(createdFiles);
        }

    }
}
