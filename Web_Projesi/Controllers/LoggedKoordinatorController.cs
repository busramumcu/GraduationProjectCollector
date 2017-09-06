using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Projesi.Models;

namespace Web_Projesi.Controllers
{
    public class LoggedKoordinatorController : Controller
    {

        // GET: LoggedKoordinator
        [Authorize(Roles = "Koordinator")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Koordinator")]
        public ActionResult Duyuru()
        {
            return View();
        }
        [Authorize(Roles = "Koordinator")]
        public ActionResult GorevListele()
        {
            using (TezProjectEntities db = new TezProjectEntities())
            {
                ViewBag.Message = TempData["Message"];
                var model = db.Gorevs.ToList();
                return View(model);
            }
        }


        [Authorize(Roles = "Koordinator")]
        public ActionResult GorevEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GorevEkle(Gorev gorev)
        {
            using (TezProjectEntities db = new TezProjectEntities())
            {
                db.Gorevs.Add(gorev);
                db.SaveChanges();
                TempData["Message"] = "Gorev Ekleme İşleme işlemi başarılı";
                return RedirectToAction("GorevListele");
            }
        }





       

        public JsonResult List()
        {
            using (TezProjectEntities db = new TezProjectEntities())
            {
                return Json(db.Duyurus.ToList(), JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Add(Duyuru duyuru)
        {
            using (TezProjectEntities db = new TezProjectEntities())
            {
                Duyuru yeniduyuru = new Duyuru();
                yeniduyuru.Duyuru_Basligi = duyuru.Duyuru_Basligi;
                yeniduyuru.Duyuru_Icerigi = duyuru.Duyuru_Icerigi;
                db.Duyurus.Add(yeniduyuru);
                db.SaveChanges();
                return Json(db.Duyurus.ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetbyID(int ID)
        {         
            using (TezProjectEntities db = new TezProjectEntities())
            {
                Duyuru duyuru = db.Duyurus.Where(x => x.Duyuru_Id.Equals(ID)).FirstOrDefault();
        
                return Json(duyuru, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Update(Duyuru gelenduyuru)
        {
            using (TezProjectEntities db = new TezProjectEntities())
            {
                Duyuru duyuru = db.Duyurus.Where(x => x.Duyuru_Id.Equals(gelenduyuru.Duyuru_Id)).FirstOrDefault();
                duyuru.Duyuru_Basligi = gelenduyuru.Duyuru_Basligi;
                duyuru.Duyuru_Icerigi = gelenduyuru.Duyuru_Icerigi;
                db.SaveChanges();
                return Json(db.Duyurus.ToList(), JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Delete(int ID)
        {
            using (TezProjectEntities db = new TezProjectEntities())
            {
                Duyuru duyuru = db.Duyurus.Where(x => x.Duyuru_Id.Equals(ID)).FirstOrDefault();
                db.Duyurus.Remove(duyuru);
                db.SaveChanges();
                return Json(db.Duyurus.ToList(), JsonRequestBehavior.AllowGet);
            }
        }

      



        [Authorize(Roles = "Koordinator")]
        public ActionResult BilgileriniDuzenle()
        {
            using (TezProjectEntities db = new TezProjectEntities())
            {
                ViewBag.Message = TempData["Message"];
                KoordinatorModel kmodel = new KoordinatorModel();
                string username = User.Identity.Name;
                kmodel.kkoordinator = db.Kullanicis.Where(x => x.Kullanici_Adi.Equals(username)).FirstOrDefault();
                kmodel.koordinator = db.Koordinators.Where(x => x.Kullanici_Id.Equals(kmodel.kkoordinator.Kullanici_Id)).FirstOrDefault();
                return View(kmodel);
            }
        }

        [HttpPost]
        public ActionResult BilgileriGuncelle(string Unvan, string Ad, string Soyad, string Sifre, string Email)
        {
            using (TezProjectEntities db = new TezProjectEntities())
            {
                string username = User.Identity.Name;
                Kullanici kullanici = db.Kullanicis.Where(x => x.Kullanici_Adi.Equals(username)).FirstOrDefault();
                Koordinator koordinator = db.Koordinators.Where(x => x.Kullanici_Id.Equals(kullanici.Kullanici_Id)).FirstOrDefault();
                koordinator.Unvan = Unvan;
                kullanici.Ad = Ad;
                kullanici.Soyad = Soyad;
                kullanici.Sifre = Sifre;
                kullanici.Email = Email;
                db.SaveChanges();
                TempData["Message"] = "Güncelleme İşlemi Başarılı";
                return RedirectToAction("BilgileriniDuzenle");
            }
        }

        [Authorize(Roles = "Koordinator")]
        public ActionResult DanismanAtama()
        {
            using (TezProjectEntities db = new TezProjectEntities())
            {
                ViewBag.Message = TempData["Message"];
                DanismanAtamaModel model = new DanismanAtamaModel();// Ogrenci ve Danisman listesi, kullanici_id ve danisman_ıd tutar;
                var ogrenciIdswithDanisman = db.Tezs.Select(s => s.Ogrenci_Id).ToArray(); // Danisman atanmıs ogrenciler
                var query = from kullanicilar in db.Kullanicis.Where(x => x.user_type == "Ogrenci")
                            where !ogrenciIdswithDanisman.Contains(kullanicilar.Kullanici_Id)
                            select kullanicilar;// Danismanı olmayan ogrencileri bulur.
                model.ogrenciler = query.ToList();
                model.danismanlar = db.Kullanicis.Where(x => x.user_type == "Danisman").ToList();
                return View(model);
            }
        }
        [HttpPost]
        public ActionResult DanismanAtamaIslemi(DanismanAtamaModel model)
        {


            using (TezProjectEntities db = new TezProjectEntities())
            {
                Tez tez = new Tez();
                tez.Ogrenci_Id = model.Kullanici_Id;
                tez.Danisman_Id = model.secilenDanismanId;
                db.Tezs.Add(tez);
                db.SaveChanges();
                TempData["Message"] = "Atama işlemi başarılı";
                return RedirectToAction("DanismanAtama");
            }


        }



        [Authorize(Roles = "Koordinator")]
        public ActionResult Onaylama()
        {
            using (TezProjectEntities database = new TezProjectEntities())
            {
                var model = new KullaniciOnayModel {
                    TumKullanicilar = database.Bekleyen_Kullanici.ToList()};                         
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Onaylama(KullaniciOnayModel model)
        {
            if (ModelState.IsValid)
            {
                using (TezProjectEntities db = new TezProjectEntities())
                {
                    for (int i = 0; i < model.SecilmisKullanicilar.Count; i++)
                    {
                        int Bekleyen_id = model.SecilmisKullanicilar[i];
                        Bekleyen_Kullanici bekleyenkullanici = db.Bekleyen_Kullanici.Where(x => x.B_Id == Bekleyen_id).FirstOrDefault();
                        if (bekleyenkullanici != null)
                        {
                            Kullanici kullanici = new Kullanici();
                            kullanici.Kullanici_Adi = bekleyenkullanici.Kullanici_Adi;                      
                            kullanici.Ad = bekleyenkullanici.Ad;
                            kullanici.Soyad = bekleyenkullanici.Soyad;
                            kullanici.Sifre = bekleyenkullanici.Sifre;
                            kullanici.Email = bekleyenkullanici.Email;
                            kullanici.user_type = bekleyenkullanici.Kullanici_Tipi;                         
                            db.Kullanicis.Add(kullanici);
                            db.Bekleyen_Kullanici.Remove(bekleyenkullanici);
                            db.SaveChanges();
                            return RedirectToAction("Success");
                        }

                    }
                }                              
            }
            return RedirectToAction("Fail");
        }
        public ActionResult Success()
        {
            return View();
        }
        public ActionResult Fail()
        {
            return View();
        }
    }
}