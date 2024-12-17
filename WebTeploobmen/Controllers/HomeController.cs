using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection;
using System.Security.Claims;
using WebTeploobmen.Data;
using WebTeploobmen.Models;

namespace WebTeploobmen.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly TeploobmenContext _context;

        public HomeController(ILogger<HomeController> logger, TeploobmenContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var userId = GetUserId();
            var variants = _context.Variants
                .Where(x => x.UserId == null || x.UserId == userId)
                .ToList();

            return View(variants);
        }



        [HttpGet]
        public IActionResult Delete(int id)
        {
            var variant = _context.Variants.FirstOrDefault(x => x.Id == id && (x.UserId == GetUserId() || x.UserId == null));

            if (variant != null)
            {
                _context.Variants.Remove(variant);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Calc(int id)
        {
            var variant = _context.Variants.FirstOrDefault(x => x.Id == id && (x.UserId == GetUserId() || x.UserId == null));
            var tableViewModel = new TableViewModel();
            var homeCalcViewModel = new HomeCalcViewModel();

            if (variant != null)
            {
                homeCalcViewModel.H0 = variant.H0;
                homeCalcViewModel.Tmal = variant.Tmal;
                homeCalcViewModel.Tbol = variant.Tbol;
                homeCalcViewModel.Wg = variant.Wg;
                homeCalcViewModel.Cg = variant.Cg;
                homeCalcViewModel.Cm = variant.Cm;
                homeCalcViewModel.Gm = variant.Gm;
                homeCalcViewModel.Av = variant.Av;
                homeCalcViewModel.Da = variant.Da;
            }

            return View(new Tuple<TableViewModel, HomeCalcViewModel>(tableViewModel, homeCalcViewModel));
        }

        [HttpPost]
        public IActionResult Calc(CalcModel model)
        {
            var S = Math.PI * Math.Pow(model.Da / 2, 2);
            var m = model.Gm * model.Cm / (model.Wg * S * model.Cg);
            var Y0 = model.Av * model.H0 / (model.Wg * model.Cg) / 1000;

            // Список для хранения строк таблицы
            var tableRows = new List<TableRow>();

            for (double y = 0; y <= model.H0; y += 0.5)
            {
                var Y = model.Av * y / (model.Wg * model.Cg) / 1000;

                // Формула 2
                var f2 = 1 - Math.Exp((m - 1) * Y / m);

                // Формула 3
                var f3 = 1 - m * Math.Exp((m - 1) * Y / m);

                // Формула 4
                var f4 = f2 / (1 - m * Math.Exp((m - 1) * Y0 / m));

                // Формула 5
                var f5 = f3 / (1 - m * Math.Exp((m - 1) * Y0 / m));

                var tMalRes = Convert.ToInt32(model.Tmal + (model.Tbol - model.Tmal) * f4);
                var tBolRes = Convert.ToInt32(model.Tmal + (model.Tbol - model.Tmal) * f5);
                var razn = tMalRes - tBolRes;

                // Добавляем строку в таблицу
                tableRows.Add(new TableRow
                {
                    Coordinate = y,
                    PelletTemperature = tMalRes,
                    GasTemperature = tBolRes,
                    TemperatureDifference = razn
                });
            }

            _context.Variants.Add(new Variant
            {
                UserId = GetUserId(),
                H0 = model.H0,
                Tmal = model.Tmal,
                Tbol = model.Tbol,
                Wg = model.Wg,
                Cg = model.Cg,
                Cm = model.Cm,
                Gm = model.Gm,
                Av = model.Av,
                Da = model.Da
            });
            _context.SaveChanges();

            return View(new Tuple<TableViewModel, HomeCalcViewModel>(
                new TableViewModel
                {
                    Rows = tableRows
                },
                new HomeCalcViewModel()
            ));
        }


        private int? GetUserId()
        {
            var userIdStr = User.FindFirst("UserId")?.Value; 
            return string.IsNullOrEmpty(userIdStr) ? (int?)null : int.Parse(userIdStr);
        }




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
