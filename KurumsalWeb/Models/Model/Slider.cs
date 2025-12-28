using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    [Table("Slider")]//Veritabanına atacağız veritabanına hangi adda bir tablo oluşturdumuzu yazıyoruz.
    public class Slider

    {
        [Key]
        public int SliderId { get; set; }
        [DisplayName("Slider başlık"),StringLength(30,ErrorMessage ="30 karakter olmalıdır")]
        public string Baslik { get; set; }
        [DisplayName("Slider açıklama"), StringLength(150, ErrorMessage = "150 karakter olmalıdır")]

        public string Aciklama { get; set; }
        [DisplayName("Slider Resim"), StringLength(250)]

        public string ResimURL { get; set; }
    }
}