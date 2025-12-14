using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    [Table("Kimlik")]
    public class Kimlik
    {
        [Key]
        public int KimlikId { get; set; }
        [DisplayName("Site başlık")]
        [Required,StringLength(100,ErrorMessage ="100 karakteer olmalıdır")]
        public String Title { get; set; }
        [DisplayName("Anahtar kelimeler")]
        [Required, StringLength(200, ErrorMessage = "200 karakteer olmalıdır")]
        public String Keywords { get; set; }
        [DisplayName("Site başlık")]
        [Required, StringLength(300, ErrorMessage = "300 karakteer olmalıdır")]
        public String Discriptinon { get; set; }
        [DisplayName("Site Logo")]
        public String LoginURL { get; set;}
        [DisplayName("Site Unvan")]
        public String Unvan { get; set; }
    }
}