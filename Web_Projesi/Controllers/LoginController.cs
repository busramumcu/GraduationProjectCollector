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
        [Authorize(Roles = "Koordinator")]
        public ActionResult IndexAdmin()
        {
            return View();
        }
        //TODO: Ayrı Controller'lara taşınacak. _Layout düzenlenecek menu eklenecek
        [Authorize(Roles = "Ogrenci")]
        public ActionResult IndexOgrenci()
        {
            return View();
        }
        [Authorize(Roles = "Ogretim Uyesi")]
        public ActionResult IndexOgretmen()
        {
            return View();
        }

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
                    return RedirectToAction("IndexAdmin");
                }
                else if (dataItem.user_type.Trim() == "Ogretim Uyesi")
                {
                    return RedirectToAction("IndexOgretmen");
                }
                else if (dataItem.user_type.Trim() == "Ogrenci")
                    return RedirectToAction("IndexOgrenci");
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