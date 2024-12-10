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
                // Если пользователь не найден, возвращаем сообщение об ошибке
                ModelState.AddModelError(string.Empty, "Неверный логин или пароль");
                return View();
            }

            // Если пользователь найден, создаем ClaimsIdentity и выполняем вход
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Login),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            // Перенаправляем на главную страницу
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
