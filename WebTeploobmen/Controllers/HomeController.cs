using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;
using WebTeploobmen.Models;

namespace WebTeploobmen.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Calc(int H0, int tmal, int Tbol, double wg, double Cg, double Cm, double Gm, double av, double Da)
        {
            var S = Math.PI * Math.Pow(Da / 2,2);

            var m = Gm * Cm / (wg * S * Cg);

            var Y0 = av * H0 / (wg * Cg) / 1000;

            //var otn_vis = 1 - m * Math.Exp(-(1 - m) * Y0 / m);

            var ymal = 3; //���������

            var Y = av * ymal / (wg * Cg) / 1000;

            //������� 2
            var f2 = 1 - Math.Exp((m - 1) * Y / m);

            //������� 3
            var f3 = 1 - m * Math.Exp((m - 1) * Y / m);

            //������� 4
            var f4 = f2 / (1 - m * Math.Exp((m - 1) * Y0 / m));

            //������� 5
            var f5 = f3 / (1 - m * Math.Exp((m - 1) * Y0 / m));

            //������� 6

            var tMalRes = Math.Round(tmal + (Tbol - tmal) * f4);

            //������� 7

            var TBolRes = Math.Round(tmal + (Tbol - tmal) * f5);

            //������� 8

            var razn = tMalRes - TBolRes;


            ViewData["razn"] = razn;

            return View();
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
