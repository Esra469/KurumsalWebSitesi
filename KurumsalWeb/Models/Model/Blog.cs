using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    [Table("Blog")]
    public class Blog
    {
        public int BlogId { get; set; }
        public string Baslik { get; set; }
        public string İcerik { get; set; }
        public string ResimURL { get; set; }
        //Sonraki 2 satır foregin key olarak yapılandırmka içn yapıldı 
        public int KategoriId { get; set; }
        public Kategori Kategori { get; set; }
    }
}