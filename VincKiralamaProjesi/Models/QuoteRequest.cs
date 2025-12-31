using System;

namespace VincKiralamaProjesi.Models
{
    public class QuoteRequest
    {
        public int Id { get; set; }

        // Ne zaman oluşturuldu
        public DateTime CreatedAt { get; set; }

        // İstenen başlangıç tarihi
        public DateTime JobStartDate { get; set; }

        // A) İş türü
        public string JobType { get; set; } = null!;
        public string? JobDescription { get; set; }

        // B) Konum
        public string City { get; set; } = null!;
        public string? District { get; set; }
        public string SiteType { get; set; } = null!;
        public string AccessType { get; set; } = null!;

        // C) Teknik bilgiler
        public int? HeightMeters { get; set; }
        public decimal? LoadWeightKg { get; set; }
        public string Duration { get; set; } = null!;

        // D) Müşteri bilgileri
        public string CustomerName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string? Email { get; set; }
        public string? CompanyName { get; set; }
        public string? Notes { get; set; }

        // E) Bu teklif hangi firmaya atanmış?
        public int? FirmId { get; set; }
        public Firm? Firm { get; set; }

        // F) İstersen seçilen vinç
        public int? CraneId { get; set; }
        public Crane? Crane { get; set; }
    }
}
