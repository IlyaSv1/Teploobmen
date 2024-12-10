using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;
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
            var variants = _context.Variants.ToList();

            return View(variants);
        }

        [HttpGet]
        public IActionResult Calc()
        {
            return View(new TableViewModel());
        }

            [HttpPost]
        public IActionResult Calc(CalcModel model)
        {
            var S = Math.PI * Math.Pow(model.Da / 2, 2);
            var m = model.Gm * model.Cm / (model.Wg * S * model.Cg);
            var Y0 = model.Av * model.H0 / (model.Wg * model.Cg) / 1000;

            // ������ ��� �������� ����� �������
            var tableRows = new List<TableRow>();

            for (double y = 0; y <= model.H0; y += 0.5)
            {
                var Y = model.Av * y / (model.Wg * model.Cg) / 1000;

                // ������� 2
                var f2 = 1 - Math.Exp((m - 1) * Y / m);

                // ������� 3
                var f3 = 1 - m * Math.Exp((m - 1) * Y / m);

                // ������� 4
                var f4 = f2 / (1 - m * Math.Exp((m - 1) * Y0 / m));

                // ������� 5
                var f5 = f3 / (1 - m * Math.Exp((m - 1) * Y0 / m));

                var tMalRes = Convert.ToInt32(model.Tmal + (model.Tbol - model.Tmal) * f4);
                var tBolRes = Convert.ToInt32(model.Tmal + (model.Tbol - model.Tmal) * f5);
                var razn = tMalRes - tBolRes;

                // ��������� ������ � �������
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

            // �������� ������� � �������������
            return View(new TableViewModel { Rows = tableRows });
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
