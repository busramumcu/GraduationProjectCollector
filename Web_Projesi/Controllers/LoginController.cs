using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Web_Projesi.Models;

namespace Web_Projesi.Controllers
{
    public class LoginController : Controller
    {// GET: Home
               

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Kullanici model, string returnUrl)
        {
            TezProjectEntities db = new TezProjectEntities();
            var dataItem = db.Kullanicis.Where(x => x.Kullanici_Adi == model.Kullanici_Adi && x.Sifre == model.Sifre).FirstOrDefault();
            if (dataItem != null)
            {
                FormsAuthentication.SetAuthCookie(dataItem.Kullanici_Adi, false);
                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                         && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))

                {
                    return Redirect(returnUrl);
                }
                else if (dataItem.user_type.Trim() == "Koordinator")
                {               
                      return RedirectToAction("Index", "LoggedKoordinator");
                }
                else if (dataItem.user_type.Trim() == "Danisman")
                {
                    return RedirectToAction("Index", "LoggedDanisman");
                }
                else if (dataItem.user_type.Trim() == "Ogrenci")
                    return RedirectToAction("Index", "LoggedOgrenci");
                else
                    return Redirect(returnUrl);
            }
            else
            {
                ModelState.AddModelError("", "Invalid user/pass");
                return View();
            }
        }
        public ActionResult Register()
        {
            ViewBag.Message = null;
            return View();
        }

        [HttpPost]
        public ActionResult Register(Bekleyen_Kullanici model)
        {
            if (ModelState.IsValid)
            {
                using (TezProjectEntities db = new TezProjectEntities())
                {
                    var gelenBekleyen = db.Bekleyen_Kullanici.Where(x => x.Kullanici_Adi == model.Kullanici_Adi).FirstOrDefault();
                    var gelenKullanici = db.Kullanicis.Where(x => x.Kullanici_Adi == model.Kullanici_Adi).FirstOrDefault();
                    if (gelenBekleyen == null && gelenKullanici == null)
                    {
                        db.Bekleyen_Kullanici.Add(model);
                        db.SaveChanges();
                        ViewBag.Message = "Kayıt onaya gonderildi";                     
                    }
                    else {
                        ViewBag.Message = "Bu kullanici adina sahip bir kullanici var. Lutfen Kullanici Adinizi Degistiriniz.";
                    }
                    
                }

            }

            //return RedirectToAction("Index","Home"); ;
            return View();
        }

        [Authorize]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}