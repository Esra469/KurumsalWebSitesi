using KurumsalWeb.Models.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.DataContext
{
    //Bu class bizim veritabanında temsil edecepi ve map edceğini sağlayan ksıımdır.
    public class KurumsalDBContext:DbContext //Dbcontextden miras aldı

    {
        public KurumsalDBContext():base("KurumsalWebDB")
        {

            
        }
        //Burada tüm tablolarımızı veritabanına set etmemiz gerekecek

        public DbSet<Admin> Admin { get; set; }
        public DbSet<Blog> Blog { get; set; }
        public DbSet<Hakkimizda> Hakkimizda { get; set; }
        public DbSet<Hizmet> Hizmet { get; set; }
        public DbSet<iletisim> iletisim { get; set; }
        public DbSet<Kategori> Kategori { get; set; }
        public DbSet<Kimlik> Kimlik { get; set; }
        public DbSet<Slider> Slider { get; set; }//yeni bir tablo ekelyeceğin zamn burdan da güncellem yapmayı unutma. aynı zammanda model ksımına da tablonun özelliklerini yaz
        public DbSet<Yorum> Yorum { get; set; }

    }
}