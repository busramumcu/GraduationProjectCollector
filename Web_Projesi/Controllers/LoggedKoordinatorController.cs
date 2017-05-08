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