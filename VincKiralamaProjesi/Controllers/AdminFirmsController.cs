using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VincKiralamaProjesi.Data;
using VincKiralamaProjesi.Services;

namespace VincKiralamaProjesi.Controllers
{
    public class AdminFirmsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly EmailService _emailService;

        public AdminFirmsController(ApplicationDbContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        // GET: /AdminFirms
        public async Task<IActionResult> Index()
        {
            var firms = await _context.Firms
                .OrderByDescending(f => f.Id)
                .ToListAsync();

            return View(firms);
        }

        // POST: /AdminFirms/Approve/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int id)
        {
            var firm = await _context.Firms.FirstOrDefaultAsync(f => f.Id == id);
            if (firm == null) return NotFound();

            // Onayla + anahtar üret
            firm.IsApproved = true;

            // firm.FirmKey yoksa üret
            if (string.IsNullOrWhiteSpace(firm.FirmKey))
            {
                firm.FirmKey = Guid.NewGuid().ToString("N")[..10].ToUpper(); // 10 karakter
            }

            await _context.SaveChangesAsync();

            // Mail gönder (hata/başarıyı ekranda görmek için)
            
            try
            {
                if (!string.IsNullOrWhiteSpace(firm.Email))
                {
                    await _emailService.SendFirmKeyAsync(firm.Email, firm.Name, firm.FirmKey);
                    TempData["MailMsg"] = $"✅ Mail gönderildi: {firm.Email}";
                }
                else
                {
                    TempData["MailMsg"] = "❌ Mail gönderilmedi: firm.Email boş.";
                }
            }
            catch (Exception ex)
            {
                TempData["MailMsg"] = "❌ Mail hatası: " + ex.Message;
            }
            

            return RedirectToAction(nameof(Index));
        }

        // POST: /AdminFirms/Reject/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(int id)
        {
            var firm = await _context.Firms.FirstOrDefaultAsync(f => f.Id == id);
            if (firm == null) return NotFound();

            firm.IsApproved = false;
            firm.FirmKey = null;

            await _context.SaveChangesAsync();

            TempData["MailMsg"] = "Firma reddedildi (mail gönderimi yok).";
            return RedirectToAction(nameof(Index));
        }
    }
}
