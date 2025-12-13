namespace KurumsalWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Kimlik")]
    public partial class Kimlik
    {
        public int KimlikId { get; set; }

        [StringLength(150)]
        public string Title { get; set; }

        [StringLength(250)]
        public string Keywords { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(250)]
        public string LogoURL { get; set; }

        [StringLength(250)]
        public string Unvan { get; set; }
    }
}
