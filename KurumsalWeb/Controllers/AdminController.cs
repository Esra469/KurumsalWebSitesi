
using KurumsalWeb.Models;
using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace KurumsalWeb.Controllers
{
    public class AdminController : Controller
    {
        //Veri tabanına erişmek için bunu kullacağız
       KurumsalDBContext db=new KurumsalDBContext();


        // GET: Admin
        [Route("yonetimpaneli")]
        public ActionResult Index()
        {
            ViewBag.BlogSay = db.Blog.Count();
            ViewBag.KategoriSay=db.Kategori.Count();
            ViewBag.HizmetSay=db.Hizmet.Count();
            ViewBag.YorumSay=db.Yorum.Count();



            ViewBag.YorumOnay = db.Yorum.Where(x => x.Onay == false).Count();
           var sorgu=db.Kategori.ToList();
           return View(sorgu);
        }

        [Route("yonetimpaneli/giris")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            var login=db.Admin.Where(x=>x.Eposta==admin.Eposta).SingleOrDefault();
            if (login.Eposta == admin.Eposta && login.Sifre==admin.Sifre)
            {
                Session["adminid"] = login.AdminId;
                Session["eposta"] = login.Eposta;
                return RedirectToAction("Index", "Admin");
            }
            ViewBag.Uyari = "Kullanıcı adı ya da şifre yanlış";
            return View(admin);
        }

        public ActionResult Logout()
        {
            //içinde herhangi bir değer görmediğinde sistem bunu düşmüş görür
            Session["adminid"] = null;
            Session["eposta"] = null;
            Session.Abandon();

            return RedirectToAction("Login", "Admin");
        }

        public ActionResult Adminler()
        {
            return View(db.Admin.ToList());
        }
        public ActionResult Create()
        {
            return View();
        
        }
        [HttpPost]
        public ActionResult Create(Admin admin,string sifre,string eposta)
        {
            if (ModelState.IsValid)
            {
                admin.Sifre = Crypto.Hash(sifre,"MD5"); /*MD5 olarak şifreyi kaydet demek*/
                db.Admin.Add(admin);
                return RedirectToAction("Adminler");
            }
            return View(admin);
        }
        public ActionResult Edit(int id)
        {
            var a=db.Admin.Where(x=>x.AdminId==id).SingleOrDefault();
            return View(a);
        }
        [HttpPost]
        public ActionResult Edit(int id,Admin admin,string sifre,string eposta) 
        {
            
            if (ModelState.IsValid)
            {
                var a = db.Admin.Where(x => x.AdminId == id).SingleOrDefault();
                a.Sifre = Crypto.Hash(sifre, "MD5");
                a.Eposta = admin.Eposta;
                a.Yetki = admin.Yetki;
                db.SaveChanges();
                return RedirectToAction("Adminler");
            }
            return View(admin);
        }
    }
}
