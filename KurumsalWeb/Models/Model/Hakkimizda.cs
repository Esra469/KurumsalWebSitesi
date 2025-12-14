using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    [Table("Hakkimizda")]
    public class Hakkimizda
    {
        //Primariy key oldupunu belirtmek için key anahtar sözcüğünü kullandık
        [Key]
        public int HakkimzidaId { get; set; }
        [Required]//boş geçemez olduğunu belirttik
        [DisplayName("Hakkımızda Açıklama")]
        public String Acıklama { get; set; }

    }
}