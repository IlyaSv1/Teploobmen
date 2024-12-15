using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using WebTeploobmen.Data;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace WebTeploobmen.Controllers
{
    public class AuthController : Controller
    {
        private readonly TeploobmenContext _context;

        public AuthController(TeploobmenContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Index(string login, string password)
        {
            var user = _context.Users.FirstOrDefault(x => x.Login == login && x.Password == password);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Неверный логин или пароль.");
                return View();
            }

            var claims = new List<Claim>
    {
        new("UserId", user.Id.ToString()),
        new Claim(ClaimTypes.Name, login),
    };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Home");
        }



        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
