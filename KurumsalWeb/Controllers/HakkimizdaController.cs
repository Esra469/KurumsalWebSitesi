using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KurumsalWeb.Controllers
{
    public class HakkimizdaController : Controller
    {
        //veritabanı bağlantısı da bu şekilde sağlanmış oldu
        KurumsalDBContext db = new KurumsalDBContext();
        // GET: Hakkimizda
        public ActionResult Index()
        {
            var h = db.Hakkimizda.ToList(); //aldığımız verileri listele

            return View(h);
        }
        public ActionResult Edit(int id)
        {
            var h = db.Hakkimizda.Where(x => x.HakkimzidaId == id).FirstOrDefault();
            return View(h);
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //editte antiforgentoken kullandığı için 
        public ActionResult Edit(int id, Hakkimizda h)
        {
            if (ModelState.IsValid)
            {
                var hakkimizda = db.Hakkimizda.Where(x => x.HakkimzidaId == id).SingleOrDefault();
                hakkimizda.Acıklama = h.Acıklama;
                db.SaveChanges();
                return RedirectToAction("Index");//nereye dönmesi gerektiğini belirtiyoruz.
            }


            return View(h);

        }
    }
}