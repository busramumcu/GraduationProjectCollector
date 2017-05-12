using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Projesi.Models;

namespace Web_Projesi.Controllers
{
    public class LoggedOgrenciController : Controller
    {
        [Authorize(Roles = "Ogrenci")]
        // GET: LoggedOgrenci
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Ogrenci")]
        public ActionResult Duyurular()
        {
            using (TezProjectEntities db = new TezProjectEntities())
            {
                var model = db.Duyurus.ToList();
                return View(model);
            }
        }
        [Authorize(Roles = "Ogrenci")]
        public ActionResult BilgileriniDuzenle()
        {
            using (TezProjectEntities db = new TezProjectEntities())
            {
                ViewBag.Message = TempData["Message"];
                OgrenciModel omodel = new OgrenciModel();
                string username = User.Identity.Name;
                omodel.kogrenci = db.Kullanicis.Where(x => x.Kullanici_Adi.Equals(username)).FirstOrDefault();
                omodel.ogrenci = db.Ogrencis.Where(x => x.Kullanici_Id.Equals(omodel.kogrenci.Kullanici_Id)).FirstOrDefault();
                return View(omodel);
            }
        }


        [HttpPost]
        public ActionResult BilgileriGuncelle(string Ogrenci_No,string Ad,string Soyad,string Sifre,string Email)
        {
            using (TezProjectEntities db = new TezProjectEntities())
            {
                string username = User.Identity.Name;
                Kullanici kullanici = db.Kullanicis.Where(x => x.Kullanici_Adi.Equals(username)).FirstOrDefault();
                Ogrenci ogrenci = db.Ogrencis.Where(x => x.Kullanici_Id.Equals(kullanici.Kullanici_Id)).FirstOrDefault();
                ogrenci.Ogrenci_No = Ogrenci_No;
                kullanici.Ad = Ad;
                kullanici.Soyad = Soyad;
                kullanici.Sifre = Sifre;
                kullanici.Email = Email;
                db.SaveChanges();
                TempData["Message"] = "Güncelleme İşlemi Başarılı";
                return RedirectToAction("BilgileriniDuzenle");
            }
        }
        


        [Authorize(Roles = "Ogrenci")]

        public ActionResult DuyuruDetay(int duyuruID)
        {
            using (TezProjectEntities db = new TezProjectEntities())
            {
                Duyuru model = db.Duyurus.Where(x => x.Duyuru_Id.Equals(duyuruID)).FirstOrDefault(); 
                return View(model);
            }

        }

        [Authorize(Roles = "Ogrenci")]
        public ActionResult GorevDeneme()
        {
            return View();
        }
    }
}