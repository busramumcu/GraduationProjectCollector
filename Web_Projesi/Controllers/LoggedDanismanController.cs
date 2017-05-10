using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Projesi.Models;

namespace Web_Projesi.Controllers
{
    public class LoggedDanismanController : Controller
    {
        [Authorize(Roles = "Danisman")]
        // GET: LoggedDanisman
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Danisman")]
        public ActionResult BilgileriniDuzenle()
        {
            using (TezProjectEntities db = new TezProjectEntities())
            {
                ViewBag.Message = TempData["Message"];
                DanismanModel omodel = new DanismanModel();
                string username = User.Identity.Name;
                omodel.kdanisman = db.Kullanicis.Where(x => x.Kullanici_Adi.Equals(username)).FirstOrDefault();
                omodel.danisman = db.Ogretim_Uyesi.Where(x => x.Kullanici_Id.Equals(omodel.kdanisman.Kullanici_Id)).FirstOrDefault();
                return View(omodel);
            }
        }


        [HttpPost]
        public ActionResult BilgileriGuncelle(string Unvan, string Ad, string Soyad, string Sifre, string Email)
        {
            using (TezProjectEntities db = new TezProjectEntities())
            {
                string username = User.Identity.Name;
                Kullanici kullanici = db.Kullanicis.Where(x => x.Kullanici_Adi.Equals(username)).FirstOrDefault();
                Ogretim_Uyesi danisman = db.Ogretim_Uyesi.Where(x => x.Kullanici_Id.Equals(kullanici.Kullanici_Id)).FirstOrDefault();
                danisman.Unvan = Unvan;
                kullanici.Ad = Ad;
                kullanici.Soyad = Soyad;
                kullanici.Sifre = Sifre;
                kullanici.Email = Email;
                db.SaveChanges();
                TempData["Message"] = "Güncelleme İşlemi Başarılı";
                return RedirectToAction("BilgileriniDuzenle");
            }
        }

    }
}