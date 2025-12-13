namespace KurumsalWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Blog")]
    public partial class Blog
    {
        public int BlogId { get; set; }

        [Required]
        [StringLength(250)]
        public string Baslik { get; set; }

        [Required]
        public string icerik { get; set; }

        [StringLength(250)]
        public string ResimURL { get; set; }

        public int? KategoriId { get; set; }

        public virtual Kategori Kategori { get; set; }
    }
}
