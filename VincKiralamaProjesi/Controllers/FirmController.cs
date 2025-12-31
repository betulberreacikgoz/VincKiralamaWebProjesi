using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VincKiralamaProjesi.Data;
using VincKiralamaProjesi.Models;
using VincKiralamaProjesi.ViewModels;

namespace VincKiralamaProjesi.Controllers
{
    public class FirmsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FirmsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Firms/Register
        public IActionResult Register()
        {
            return View(new FirmRegisterViewModel());
        }

        // POST: /Firms/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(FirmRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var firm = new Firm
            {
                Name = model.Name,
                Phone = model.Phone,
                Email = model.Email,
                City = model.City,
                District = model.District,
                Address = model.Address,
                CreatedAt = DateTime.Now,
                IsApproved = false, // admin onaylayana kadar pasif
                FirmKey = null      // onaylanırken üretilecek
            };

            _context.Firms.Add(firm);
            await _context.SaveChangesAsync();

            return View("RegisterResult");
        }
    }
}
