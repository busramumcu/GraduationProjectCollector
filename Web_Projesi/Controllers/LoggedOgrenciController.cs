using System;
using System.Collections.Generic;
using System.IO;
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

        public ActionResult DuyuruDetay(int duyuruID)
        {
            using (TezProjectEntities db = new TezProjectEntities())
            {
                Duyuru model = db.Duyurus.Where(x => x.Duyuru_Id.Equals(duyuruID)).FirstOrDefault();
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
        public ActionResult BilgileriGuncelle(string Ogrenci_No, string Ad, string Soyad, string Sifre, string Email)
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
        public ActionResult DanismanOnayBekleme()
        {
            ViewBag.Message = TempData["Message"];
            return View();
        }


        [Authorize(Roles = "Ogrenci")]
        public ActionResult TezDuzenle()
        {
            using (TezProjectEntities db = new TezProjectEntities())
            {
                ViewBag.Message = TempData["Message"];
                Tez tez = new Tez();
                string username = User.Identity.Name;
                Kullanici kullanici = db.Kullanicis.Where(x => x.Kullanici_Adi.Equals(username)).FirstOrDefault();
                tez = db.Tezs.Where(x => x.Ogrenci_Id.Equals(kullanici.Kullanici_Id)).FirstOrDefault();
                if (tez == null)
                {
                    TempData["Message"] = "Ogrenci Danisman Ataması Beklemektedir";
                    return RedirectToAction("DanismanOnayBekleme");
                }
                else
                {
                    return View(tez);
                }
            }
        }
        [HttpPost]
        public ActionResult TezGuncelle(string Ogrenci_Id, string Danisman_Id, string Abstract, string Donem)
        {
            using (TezProjectEntities db = new TezProjectEntities())
            {
                string username = User.Identity.Name;
                Tez tez = new Tez();
                Kullanici kullanici = db.Kullanicis.Where(x => x.Kullanici_Adi.Equals(username)).FirstOrDefault();
                tez = db.Tezs.Where(x => x.Ogrenci_Id.Equals(kullanici.Kullanici_Id)).FirstOrDefault();
                tez.Abstract = Abstract;
                tez.Donem = Donem;
                db.SaveChanges();
                TempData["Message"] = "Güncelleme İşlemi Başarılı";
                return RedirectToAction("TezDuzenle");
            }
        }

        [Authorize(Roles = "Ogrenci")]
        public ActionResult OgrenciGorevListele()
        {
            using (TezProjectEntities db = new TezProjectEntities())
            {
                ViewBag.Message = TempData["Message"];
                var model = db.Gorevs.ToList();
                return View(model);
            }
        }
        [Authorize(Roles = "Ogrenci")]
        public ActionResult GorevDosyaYukle(int gorev_Id)
        {
            using (TezProjectEntities db = new TezProjectEntities())
            {
                GorevModel model = new GorevModel();
                model.Kullanici_Id = db.Kullanicis.Where(x => x.Kullanici_Adi == User.Identity.Name).FirstOrDefault().Kullanici_Id;
                model.Gorev_Id = gorev_Id;
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult DosyaYukle(string Gorev_Id, string Kullanici_Id, HttpPostedFileBase dosya)
        {
            using (TezProjectEntities db = new TezProjectEntities())
            {
                try
                {
                    string dosyaYolu = Gorev_Id + Kullanici_Id + Path.GetFileName(dosya.FileName);
                    var yuklemeYeri = Path.Combine(Server.MapPath("~/UploadedFiles"), dosyaYolu);
                    dosya.SaveAs(yuklemeYeri);
                    Dosya dosyam = new Dosya();
                    dosyam.Gorev_Id = Convert.ToInt32(Gorev_Id);
                    dosyam.Kullanici_Id = Convert.ToInt32(Kullanici_Id);
                    dosyam.Dosya_Adi = dosyaYolu;
                    dosyam.Yukleme_Yeri = yuklemeYeri;
                    dosyam.Dosya_Uzantisi = Path.GetExtension(dosya.FileName);
                    //TODO: Dosya Uzantısı kontrol edilecek.
                    db.Dosyas.Add(dosyam);
                    db.SaveChanges();
                    TempData["Message"] = Gorev_Id + " Idli gorev için dosya Yukleme işlemi Başarılı";
                    return RedirectToAction("OgrenciGorevListele");
                }
                catch (Exception)
                {
                    TempData["Message"] = Gorev_Id + " Idli gorev için dosya Yukleme işlemi Başarısız";
                    return RedirectToAction("OgrenciGorevListele");
                }
             
            }


        }

        [Authorize(Roles = "Ogrenci")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DosyaYukleme(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
                try
                {
                    string path = Path.Combine(Server.MapPath("~/App_Data"),
                                               Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                    ViewBag.Message = "File uploaded successfully";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }
            return View();
        }

    }
}