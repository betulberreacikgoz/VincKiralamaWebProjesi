using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VincKiralamaProjesi.Data;
using VincKiralamaProjesi.Models;

namespace VincKiralamaProjesi.Controllers
{
    public class FirmPanelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FirmPanelController(ApplicationDbContext context)
        {
            _context = context;
        }

        private int? CurrentFirmId => HttpContext.Session.GetInt32("FirmId");

        private IActionResult FirmLoginRedirect()
            => RedirectToAction("Login", "FirmAuth");

        // GET: /FirmPanel/Dashboard
        public IActionResult Dashboard()
        {
            if (CurrentFirmId == null) return FirmLoginRedirect();
            ViewBag.FirmName = HttpContext.Session.GetString("FirmName");
            return View();
        }

        // GET: /FirmPanel/MyCranes
        public async Task<IActionResult> MyCranes()
        {
            if (CurrentFirmId == null) return FirmLoginRedirect();

            var firmId = CurrentFirmId.Value;

            var cranes = await _context.Cranes
                .Include(c => c.Category)
                .Where(c => c.FirmId == firmId)
                .OrderByDescending(c => c.Id)
                .ToListAsync();

            return View(cranes);
        }

        // GET: /FirmPanel/CreateCrane
        public async Task<IActionResult> CreateCrane()
        {
            if (CurrentFirmId == null) return FirmLoginRedirect();

            ViewBag.Categories = await _context.Categories.OrderBy(x => x.Name).ToListAsync();
            return View(new Crane());
        }

        // POST: /FirmPanel/CreateCrane
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCrane([Bind("Name,CapacityTon,DailyPrice,Description,ImageUrl,CategoryId,City,District")] Crane model)
        {
            if (CurrentFirmId == null) return FirmLoginRedirect();

            // İlişkisel alan hatalarını temizle
            if (ModelState.ContainsKey("Category")) ModelState.Remove("Category");
            if (ModelState.ContainsKey("Firm")) ModelState.Remove("Firm");

            // Resim URL boşsa varsayılan ata
            if (string.IsNullOrWhiteSpace(model.ImageUrl))
            {
                model.ImageUrl = "https://via.placeholder.com/400x300?text=Vinc+Resmi";
                if (ModelState.ContainsKey("ImageUrl")) ModelState.Remove("ImageUrl");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _context.Categories.OrderBy(x => x.Name).ToListAsync();
                return View(model);
            }

            model.FirmId = CurrentFirmId.Value;

            _context.Cranes.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(MyCranes));
        }

        // POST: /FirmPanel/DeleteCrane/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCrane(int id)
        {
            if (CurrentFirmId == null) return FirmLoginRedirect();

            var firmId = CurrentFirmId.Value;

            var crane = await _context.Cranes.FirstOrDefaultAsync(c => c.Id == id && c.FirmId == firmId);
            if (crane == null) return NotFound();

            _context.Cranes.Remove(crane);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(MyCranes));
        }

        // GET: /FirmPanel/EditCrane/5
        public async Task<IActionResult> EditCrane(int id)
        {
            if (CurrentFirmId == null) return FirmLoginRedirect();

            var firmId = CurrentFirmId.Value;
            var crane = await _context.Cranes.FirstOrDefaultAsync(c => c.Id == id && c.FirmId == firmId);

            if (crane == null) return NotFound();

            ViewBag.Categories = await _context.Categories.OrderBy(x => x.Name).ToListAsync();
            return View(crane);
        }

        // POST: /FirmPanel/EditCrane/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCrane(int id, [Bind("Id,Name,CapacityTon,DailyPrice,Description,ImageUrl,CategoryId,City,District")] Crane model)
        {
            if (CurrentFirmId == null) return FirmLoginRedirect();
            if (id != model.Id) return NotFound();

            var firmId = CurrentFirmId.Value;
            var existingCrane = await _context.Cranes.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id && c.FirmId == firmId);

            if (existingCrane == null) return NotFound();

            // İlişkisel alan hatalarını temizle
            if (ModelState.ContainsKey("Category")) ModelState.Remove("Category");
            if (ModelState.ContainsKey("Firm")) ModelState.Remove("Firm");

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _context.Categories.OrderBy(x => x.Name).ToListAsync();
                return View(model);
            }

            // FirmId'nin değişmediğinden emin oluyoruz (Güvenlik)
            model.FirmId = firmId;

            _context.Update(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(MyCranes));
        }

        // GET: /FirmPanel/MyQuotes
        public async Task<IActionResult> MyQuotes()
        {
            if (CurrentFirmId == null) return FirmLoginRedirect();

            var firmId = CurrentFirmId.Value;

            var quotes = await _context.QuoteRequests
                .Include(q => q.Crane)
                .Where(q => q.FirmId == firmId)
                .OrderByDescending(q => q.CreatedAt)
                .ToListAsync();

            return View(quotes);
        }

        // GET: /FirmPanel/QuoteDetails/5
        public async Task<IActionResult> QuoteDetails(int id)
        {
            if (CurrentFirmId == null) return FirmLoginRedirect();

            var firmId = CurrentFirmId.Value;

            var quote = await _context.QuoteRequests
                .Include(q => q.Crane)
                .FirstOrDefaultAsync(q => q.Id == id && q.FirmId == firmId);

            if (quote == null) return NotFound();

            return View(quote);
        }
    }
}
