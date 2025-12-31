using Microsoft.EntityFrameworkCore;
using VincKiralamaProjesi.Models;

namespace VincKiralamaProjesi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Crane> Cranes { get; set; }
        public DbSet<Firm> Firms { get; set; }
        public DbSet<QuoteRequest> QuoteRequests { get; set; }

    }
}
