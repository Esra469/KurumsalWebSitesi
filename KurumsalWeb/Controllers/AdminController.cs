
using KurumsalWeb.Models;
using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;

namespace KurumsalWeb.Controllers
{
    public class AdminController : Controller
    {
        //Veri tabanına erişmek için bunu kullacağız
       KurumsalDBContext db=new KurumsalDBContext();

        // GET: Admin
        public ActionResult Index()
        {
        var sorgu=db.Kategori.ToList();
           return View(sorgu);
        }

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
    }
}
