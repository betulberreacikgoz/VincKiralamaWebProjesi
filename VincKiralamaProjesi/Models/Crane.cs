using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VincKiralamaProjesi.Models
{
    public class Crane
    {
       public int Id { get; set; }

        public string Name { get; set; } = null!;
        public int CapacityTon { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DailyPrice { get; set; }
        public string Description { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;

        // Kategori
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        // Firma (şimdilik nullable bıraktım, migration’da sorun çıkmasın)
        public int? FirmId { get; set; }
        public Firm? Firm { get; set; }

        // Konum (basit versiyon)
        public string? City { get; set; }      // İstanbul, Ankara...
        public string? District { get; set; }  // Kadıköy, Çankaya...
    }
}
