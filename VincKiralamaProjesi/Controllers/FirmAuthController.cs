using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VincKiralamaProjesi.Data;

namespace VincKiralamaProjesi.Controllers
{
    public class FirmAuthController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FirmAuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /FirmAuth/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /FirmAuth/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string firmKey)
        {
            if (string.IsNullOrWhiteSpace(firmKey))
            {
                ViewBag.Error = "Firma anahtarı boş olamaz.";
                return View();
            }

            firmKey = firmKey.Trim().ToUpper();

            var firm = await _context.Firms
                .FirstOrDefaultAsync(f => f.FirmKey != null && f.FirmKey.ToUpper() == firmKey);

            if (firm == null)
            {
                ViewBag.Error = "Firma anahtarı bulunamadı.";
                return View();
            }

            if (!firm.IsApproved)
            {
                ViewBag.Error = "Firma henüz onaylanmamış.";
                return View();
            }

            // Session'a firmayı yaz
            HttpContext.Session.SetInt32("FirmId", firm.Id);
            HttpContext.Session.SetString("FirmName", firm.Name);

            return RedirectToAction("Dashboard", "FirmPanel");
        }

        // GET: /FirmAuth/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
