using Microsoft.AspNetCore.Mvc;
using System.Linq;
using VincKiralamaProjesi.Data;  // Kendi Context namespace'iniz
using VincKiralamaProjesi.Models; // Kendi Models namespace'iniz

namespace VincKiralamaProjesi.Controllers
{
    [Route("api/mobile/auth")]
    [ApiController]
    public class MobileAuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MobileAuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ============================
        // 1. FİRMA GİRİŞİ
        // ============================
        [HttpPost("login/firm")]
        public IActionResult LoginFirm([FromBody] FirmLoginDto model)
        {
            var firm = _context.Firms.FirstOrDefault(x => x.Email == model.Email);
            
            if (firm != null)
            {
                return Ok(new { 
                    Id = firm.Id.ToString(), 
                    Email = firm.Email, 
                    Role = "Firma", 
                    FirmName = firm.Name, 
                    Token = "firm_token_123"
                });
            }
            return Unauthorized(new { Message = "Firma bulunamadı" });
        }

        // ============================
        // 2. ADMIN GİRİŞİ (Sanal)
        // ============================
        [HttpPost("login/admin")]
        public IActionResult LoginAdmin([FromBody] AdminLoginDto model)
        {
            // Veritabanında admin tablosu olmadığı için kodla kontrol ediyoruz
            if (model.Email == "admin@gmail.com" && model.Password == "12345")
            {
                return Ok(new {
                    Id = "999",
                    Email = "admin@gmail.com",
                    Role = "Admin",
                    Token = "admin_master_token"
                });
            }
            return Unauthorized(new { Message = "Admin şifresi hatalı" });
        }

        // ============================
        // 3. FİRMALARI LİSTELE (Admin İçin)
        // ============================
        [HttpGet("firms")]
        public IActionResult GetAllFirms()
        {
             var list = _context.Firms.ToList();
    return Ok(list.Select(f => new {
        Id = f.Id,
        Name = f.Name,
        Email = f.Email,
        Phone = f.Phone,
        ApiKey = f.FirmKey, // <-- BURASI DÜZELDİ: FirmKey oldu
        IsApproved = true // Varsa f.IsApproved
    }));
        }
    }

    // DTO Sınıfları
    public class FirmLoginDto {
        public string Email { get; set; }
        public string ApiKey { get; set; }
    }

    public class AdminLoginDto {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}