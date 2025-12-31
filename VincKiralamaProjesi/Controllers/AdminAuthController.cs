using Microsoft.AspNetCore.Mvc;

namespace VincKiralamaProjesi.Controllers
{
    public class AdminAuthController : Controller
    {
        private readonly IConfiguration _config;

        public AdminAuthController(IConfiguration config)
        {
            _config = config;
        }

        // GET: /AdminAuth/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /AdminAuth/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string password)
        {
            var adminPass = _config["Admin:Password"];

            if (string.IsNullOrWhiteSpace(password) || password != adminPass)
            {
                ViewBag.Error = "Şifre yanlış.";
                return View();
            }

            HttpContext.Session.SetString("IsAdmin", "true");
            return RedirectToAction("Index", "AdminFirms"); // istersen başka admin ana sayfa
        }

        // /AdminAuth/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("IsAdmin");
            return RedirectToAction("Login");
        }
    }
}
