using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VincKiralamaProjesi.Data;
using VincKiralamaProjesi.Models;
using VincKiralamaProjesi.Filters;

namespace VincKiralamaProjesi.Controllers
{
    [AdminOnly]
    public class QuoteRequestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuoteRequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /QuoteRequests
        // Tüm teklifleri listeleyen admin sayfası
        public async Task<IActionResult> Index()
        {
            var quotes = await _context.QuoteRequests
                .OrderByDescending(q => q.CreatedAt)
                .ToListAsync();

            return View(quotes);
        }

        // GET: /QuoteRequests/Details/5
        // Tek bir teklifi detaylı gösterir
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var quote = await _context.QuoteRequests
                .FirstOrDefaultAsync(q => q.Id == id);

            if (quote == null)
                return NotFound();

            return View(quote);
        }

        // İstersen ileride Delete / Edit ekleyebiliriz
    }
}
