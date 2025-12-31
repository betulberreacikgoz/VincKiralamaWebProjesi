using System.ComponentModel.DataAnnotations;

namespace VincKiralamaProjesi.ViewModels
{
    public class FirmRegisterViewModel
    {
        [Display(Name = "Firma Adı")]
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        public string Name { get; set; } = null!;

        [Display(Name = "Telefon")]
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        public string Phone { get; set; } = null!;

        [Display(Name = "E-posta Adresi")]
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; } = null!;   // 🔴 ZORUNLU

        [Display(Name = "Şehir")]
        [Required(ErrorMessage = "{0} seçimi zorunludur.")]
        public string City { get; set; } = null!;

        [Display(Name = "İlçe")]
        [Required(ErrorMessage = "{0} seçimi zorunludur.")]
        public string District { get; set; } = null!;

        [Display(Name = "Adres")]
        public string? Address { get; set; }
    }
}
