namespace VincKiralamaProjesi.ViewModels
{
    public class QuoteRequestViewModel
    {
        // A) İş türü
        public DateTime JobStartDate { get; set; }     // İşin yapılacağı tarih
        public string JobType { get; set; } = null!;   // cam silme, eşya taşıma vb.
        public string? JobDescription { get; set; }    // Diğer seçildiyse açıklama

        // B) Konum
        public string City { get; set; } = null!;
        public string? District { get; set; }
        public string SiteType { get; set; } = null!;  // dar sokak, şantiye vb.
        public string AccessType { get; set; } = null!; // bina dibine yaklaşabilir vb.

        // C) Teknik ihtiyaç
        public int? HeightMeters { get; set; }         // kaç metre
        public decimal? LoadWeightKg { get; set; }     // kaç kg/ton
        public string Duration { get; set; } = null!;  // 1 gün, 1 hafta...

        // D) Müşteri
        public string CustomerName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string? Email { get; set; }
        public string? CompanyName { get; set; }
        public string? Notes { get; set; }
    }
}
