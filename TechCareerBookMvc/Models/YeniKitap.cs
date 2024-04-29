using System.ComponentModel.DataAnnotations;

namespace TechCareerBookMvc.Models
{
    public class YeniKitap
    {
        [Key]
        public int Id { get; set; }

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