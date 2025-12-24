using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace KurumsalWeb.Controllers
{
    public class BlogController : Controller
    {
        private KurumsalDBContext  db=new KurumsalDBContext();
        // GET: Blog
        public ActionResult Index()

        {
           
            return View(db.Blog.ToList());
        }
        public ActionResult Create()
        {
            ViewBag.KategoriId = new SelectList(db.Kategori, "KategoriId","KategoriAd");//Veri çekme için viewbag, veri taşıma işlemleri
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Blog blog,HttpPostedFileBase ResimURL)
        {

            if (ResimURL != null)
            {
                
                WebImage img = new WebImage(ResimURL.InputStream);
                FileInfo imginfo = new FileInfo(ResimURL.FileName);

                // string logoname = LoginURL.FileName + imginfo.Extension; //masaüstüne nasıl kaydetmişsek o şekilde veritabanına kayıt olacak
                string blogimgname = Guid.NewGuid().ToString() + imginfo.Extension;//otomatik resmi isimlendirecek
                img.Resize(600, 400);
                img.Save("~/Uploads/Blog/" + blogimgname);

                blog.ResimURL = "/Uploads/Blog/" + blogimgname;

            }
            db.Blog.Add(blog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}