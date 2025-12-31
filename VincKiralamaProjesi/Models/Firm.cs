using System;
using System.Collections.Generic;

namespace VincKiralamaProjesi.Models
{
    public class Firm
    {
        public int Id { get; set; }

        // Temel bilgiler
        public string Name { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Email { get; set; }

        // Adres / konum
        public string City { get; set; } = null!;
        public string? District { get; set; }
        public string? Address { get; set; }

        // Kayıt ve onay durumu
        public DateTime CreatedAt { get; set; }
        public bool IsApproved { get; set; }

        // Admin’in vereceği “anahtar” (firma giriş etiketi gibi)
        public string? FirmKey { get; set; }

        // Bu firmanın sahip olduğu vinçler
        public ICollection<Crane> Cranes { get; set; } = new List<Crane>();
    }
}
