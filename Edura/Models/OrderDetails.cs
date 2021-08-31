using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Edura.Models
{
    public class OrderDetails
    {
        [Required(ErrorMessage="Lütfen adres tanımı giriniz.")]
        public string AdresTanimi { get; set; }
        [Required(ErrorMessage = "Lütfen adres  giriniz.")]
        public string Adres { get; set; }
        [Required(ErrorMessage = "Lütfen sehir adı giriniz.")]
        public string Sehir { get; set; }
        [Required(ErrorMessage = "Lütfen semt adı giriniz.")]
        public string Semt { get; set; }
        [Required(ErrorMessage = "Lütfen telefon numarası giriniz.")]
        public string Telefon { get; set; }

    }
}
