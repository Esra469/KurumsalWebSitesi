using KurumsalWeb.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KurumsalWeb.Controllers
{
    public class HomeController : Controller
    {
        private  KurumsalDBContext db=new KurumsalDBContext();
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Hizmetler = db.Hizmet.ToList().OrderByDescending(x => x.HizmetId);
            ViewBag.İletisim=db.iletisim.SingleOrDefault();

            ViewBag.Blog = db.Blog.OrderByDescending(x => x.BlogId).ToList();

            return View();
        }
        public ActionResult SliderPartial()
        {
            var veriler = db.Slider
                    .OrderByDescending(x => x.SliderId) 
                    .ToList();

            return View(veriler);//bu şekidle slider tablosu bize gelmiş olur
        }
        public ActionResult HizmetPartial()
        {
            return View(db.Hizmet.ToList());
        }
      
    }
}