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
            
            db.Configuration.LazyLoadingEnabled = false;//Sistemde listeleme yapmak istediğimiz zamn göremiyoruz. listelemeyi yapabilmek için lazyloadingenable kapalı olması gerek bu yüzden false yapıyoruz

            //Burada da üste yazdığımız koddan dolayı Include("kategori tablosunu geleceğini belirtiyoruz.") listeleme iişlemi bu şekilde gerçekleşecektir.
            return View(db.Blog.Include("Kategori").ToList().OrderByDescending(x=>x.BlogId));
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

        public ActionResult Edit(int id)
        {
            if(id== null)
            {
                return HttpNotFound();
            }
            var b = db.Blog.Where(x=>x.BlogId==id).FirstOrDefault();//tek kayıt olacağı için single or Default diye belirttik
            if(b == null)
            {
                return HttpNotFound();

            }
            ViewBag.KategoriId = new SelectList(db.Kategori, "KategoriId", "KategoriAd", b.KategoriId);
            return View(b);
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,Blog blog,HttpPostedFileBase ResimURL)
        {
            if(ModelState.IsValid)
            {
                var b=db.Blog.Where(x=>x.BlogId==id).SingleOrDefault();


                if (ResimURL != null)
                {
                   
                    if (!string.IsNullOrEmpty(b.ResimURL))
                    {
                        var oldFilePath = Server.MapPath("~/" + b.ResimURL);

                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }
                    WebImage img = new WebImage(ResimURL.InputStream);
                    FileInfo imginfo = new FileInfo(ResimURL.FileName);

                    // string logoname = LoginURL.FileName + imginfo.Extension; //masaüstüne nasıl kaydetmişsek o şekilde veritabanına kayıt olacak
                    string blogimgname = ResimURL.FileName;
                    img.Resize(600, 400);
                    img.Save("~/Uploads/Blog/" + blogimgname);

                    b.ResimURL = "/Uploads/Blog/" + blogimgname;

                }
                b.Baslik=blog.Baslik;
                b.İcerik = blog.İcerik;
                b.KategoriId= blog.KategoriId;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(blog);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var b=db.Blog.Find(id);
            if(b == null)
            {
                return HttpNotFound();

            }

            if (System.IO.File.Exists(Server.MapPath(b.ResimURL)))
            {
                System.IO.File.Delete(Server.MapPath(b.ResimURL));
            }
            db.Blog.Remove(b);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}