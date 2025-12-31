using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VincKiralamaProjesi.Data;
using VincKiralamaProjesi.Models;

namespace VincKiralamaProjesi.Controllers
{
    public class CranesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CranesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // /Cranes veya /Cranes?categoryId=1
        public IActionResult Index(int? categoryId)
        {
            var query = _context.Cranes
                .Include(c => c.Category)
                .AsQueryable();

            if (categoryId.HasValue)
            {
                query = query.Where(c => c.CategoryId == categoryId.Value);
            }

            var cranes = query.ToList();
            return View(cranes);
        }

        // /Cranes/Details/5
        public IActionResult Details(int id)
        {
            var crane = _context.Cranes
                .Include(c => c.Category)
                .FirstOrDefault(c => c.Id == id);

            if (crane == null)
                return NotFound();

            return View(crane);
        }
    }
}
