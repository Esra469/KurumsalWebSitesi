using KurumsalWeb.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Management;
using System.Web.Mvc;
using System.EnterpriseServices.Internal;
using PagedList;
using PagedList.Mvc;

namespace KurumsalWeb.Controllers
{
    public class HomeController : Controller
    {
        private KurumsalDBContext db = new KurumsalDBContext();
        // GET: Home
        public ActionResult Index(int Sayfa=1)
        {
            ViewBag.Hizmetler = db.Hizmet.ToList().OrderByDescending(x => x.HizmetId);
            


            return View();
        }
        public ActionResult SliderPartial()
        {
            var veriler = db.Slider.OrderByDescending(x => x.SliderId).ToList();

            return View(veriler);//bu şekidle slider tablosu bize gelmiş olur
        }
        public ActionResult HizmetPartial()
        {
            return View(db.Hizmet.ToList());
        }
        public ActionResult Hakkimizda()
        {
            return View(db.Hakkimizda.SingleOrDefault());
        
        }
      public ActionResult Hizmetlerimiz()
        {
            return View(db.Hizmet.ToList().OrderByDescending(x=>x.HizmetId));
        }

        public ActionResult Iletisim()
        {
            return View(db.iletisim.SingleOrDefault());
        }

        [HttpPost]
        public ActionResult Iletisim(string adsoyad=null,string email=null,string konu=null,string mesaj=null)
        {
            //Bu uyarıları ayrıca görünmesi için bunların iletişim.cshtml kısmında da tanımlanması gerekiyor.
                if (adsoyad != null && email != null)
                {
                    try
                    {
                       
                        SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                        client.EnableSsl = true; // Güvenli bağlantı

                        client.Credentials = new NetworkCredential("mailadresi@gmail.com", "16hanelikyenisifre");

                        MailMessage mail = new MailMessage();
                        mail.From = new MailAddress("mailadresi@gmail.com", "Site İletişim Formu"); // Kimden gidiyor
                        mail.To.Add("mailadresi@gmail.com"); // Kime gidecek (Yine kendimize gönderiyoruz)
                        mail.Subject = konu + " - " + adsoyad; // Mail Başlığı
                        mail.IsBodyHtml = true;
                        mail.Body = $"Gönderen: {adsoyad} ({email}) <br/> Mesaj: {mesaj}";

                        client.Send(mail);

                        ViewBag.Uyari = "Mesajınız başarıyla gönderildi.";
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Uyari = "Hata oluştu: " + ex.Message;
                    Console.WriteLine("hata oluştu") ;
                    }
                }
                else
                {
                    ViewBag.Uyari = "Lütfen tüm alanları doldurunuz.";
                }

                return View();
            
        }

        public ActionResult Blog(int Sayfa=1)
        {
            //kategoriye bağımlı olduğu için kategoriyi de include olarak ekleme yapıyoruz
            //return View(db.Blog.Include("Kategori").ToList().OrderByDescending(x=>x.BlogId));


            //pagetlist ile düzenlendikten sonra
            return View(db.Blog.Include("Kategori").OrderByDescending(x => x.BlogId).ToPagedList(Sayfa,6));
        }

        public ActionResult BlogDetay(int? id)
        {
            var b=db.Blog.Include("Kategori").Where(x=>x.BlogId==id).SingleOrDefault();
            return View(b);
        }

        public ActionResult BlogKategoriPartial()
        {
           // db.Configuration.LazyLoadingEnabled = false;//bak

            return PartialView(db.Kategori.Include("Blogs").ToList().OrderBy(x => x.KategoriAd));
        }
        public ActionResult FooterPartial()
        {

            ViewBag.Hizmetler = db.Hizmet.ToList().OrderByDescending(x => x.HizmetId);
            var iletisim = db.iletisim.FirstOrDefault();//model olarak alacağımız için viewbag olarak belirtemeyiz.

            ViewBag.Blog = db.Blog.OrderByDescending(x => x.BlogId).ToList();
            return PartialView(iletisim);
        }

    }  
}