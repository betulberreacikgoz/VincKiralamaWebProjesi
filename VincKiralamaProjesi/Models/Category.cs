using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VincKiralamaProjesi.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kategori adı zorunludur.")]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        // İlişki: Bir kategoriye bağlı bir sürü vinç olabilir
        public ICollection<Crane> Cranes { get; set; }
    }
}
