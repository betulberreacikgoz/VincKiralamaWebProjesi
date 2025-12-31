using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VincKiralamaProjesi.Data;
using VincKiralamaProjesi.Models;
using VincKiralamaProjesi.ViewModels;

namespace VincKiralamaProjesi.Controllers
{
    public class QuotesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuotesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Quotes/Create
        public IActionResult Create()
        {
            return View(new QuoteRequestViewModel());
        }

        // POST: /Quotes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(QuoteRequestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // 1) İş türüne göre kategori eşlemesi
            string? categoryName = model.JobType switch
            {
                "Cam silme / dış cephe"   => "Sepetli Vinç",
                "Eşya / makine taşıma"    => "Mobil Vinç",
                "Çatı / klima montajı"    => "Sepetli Vinç",
                "İnşaat kalıp / beton"    => "Kule Vinç",
                "Reklam tabelası montajı" => "Sepetli Vinç",
                "Ağaç kesimi / peyzaj"    => "Sepetli Vinç",
                _ => null
            };

            // 2) Teklifi oluştur ve DB'ye ekle
            var entity = new QuoteRequest
            {
                CreatedAt = DateTime.Now,
                JobType = model.JobType,
                JobDescription = model.JobDescription,
                City = model.City,
                District = model.District,
                SiteType = model.SiteType,
                AccessType = model.AccessType,
                HeightMeters = model.HeightMeters,
                LoadWeightKg = model.LoadWeightKg,
                Duration = model.Duration,
                JobStartDate = model.JobStartDate,
                CustomerName = model.CustomerName,
                Phone = model.Phone,
                Email = model.Email,
                CompanyName = model.CompanyName,
                Notes = model.Notes
            };

            _context.QuoteRequests.Add(entity);
            await _context.SaveChangesAsync(); // ID burada oluşuyor

            // 3) Vinçleri filtrele
            var query = _context.Cranes
                .Include(c => c.Category)
                .Include(c => c.Firm)
                .AsQueryable();

            // Kategori kısıtı
            if (!string.IsNullOrWhiteSpace(categoryName))
                query = query.Where(c => c.Category.Name == categoryName);

            // Kapazite kısıtı
            if (model.LoadWeightKg.HasValue && model.LoadWeightKg.Value > 0)
            {
                var tons = (int)Math.Ceiling(model.LoadWeightKg.Value / 1000m);
                query = query.Where(c => c.CapacityTon >= tons);
            }

            // Şehir kısıtı (Zorunlu) - En azından aynı şehirde olmalı
            string targetCity = model.City?.Trim() ?? "";
            string targetDistrict = model.District?.Trim() ?? "";

            if (!string.IsNullOrWhiteSpace(targetCity))
            {
                // Contains kullanarak "İstanbul " gibi boşluk hatalarını tolere ediyoruz
                query = query.Where(c => c.City.Contains(targetCity));
            }

            // DB'den veriyi çekiyoruz (Client-side evaluation ile sıralama yapmak daha güvenli)
            var candidates = await query.ToListAsync();

            // 4) Akıllı Sıralama (Puanlama Sistemi)
            // Hedefimiz:
            // 1. İlçe Eşleşiyor mu? (En önemli)
            // 2. Fiyat (Ucuzdan pahalıya)
            var suggested = candidates
                .OrderByDescending(c => !string.IsNullOrWhiteSpace(targetDistrict) && 
                                        c.District != null && 
                                        c.District.Contains(targetDistrict, StringComparison.OrdinalIgnoreCase)) // İlçe tutanlar üste
                .ThenBy(c => c.DailyPrice) // Sonra fiyata göre
                .Take(10)
                .ToList();

            // 5) İlk uygun vinci bu teklife bağla
            if (suggested.Any())
            {
                var best = suggested.First();   // en uygun vinç

                entity.FirmId = best.FirmId;    // teklifi firmaya bağla
                entity.CraneId = best.Id;       // hangi vince ait olduğunu kaydet

                await _context.SaveChangesAsync(); // güncelle
            }

            // 6) ViewModel ile sonuç sayfasına git
            var vm = new QuoteSuggestionsViewModel
            {
                Request = model,
                SuggestedCranes = suggested
            };

            return View("Suggestions", vm);
        }
    }
}
