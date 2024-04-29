using System.ComponentModel.DataAnnotations;

namespace TechCareerBookMvc.ViewModels
{
    public class YeniKitapViewModel : EditImageViewModel
    {
        [Required]
        public string kitapAdi { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        public decimal fiyat { get; set; }

        [Required]
        public string kitapResim { get; set; }

        [Required]
        public string yayinlanmaTarihi { get; set; }
    }
}
