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
        [Authorize(Roles = "Admin")]
        public ActionResult IndexAdmin()
        {
            return View();
        }
        //TODO: Ayrı Controller'lara taşınacak. _Layout düzenlenecek menu eklenecek
        [Authorize(Roles = "Student")]
        public ActionResult IndexOgrenci()
        {
            return View();
        }
        [Authorize(Roles = "Teacher")]
        public ActionResult IndexOgretmen()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(tblLogin model, string returnUrl)
        {
            TezProjectEntities db = new TezProjectEntities();
            var dataItem = db.tblLogins.Where(x => x.Username == model.Username && x.Password == model.Password).FirstOrDefault();
            if (dataItem != null)
            {
                FormsAuthentication.SetAuthCookie(dataItem.Username, false);
                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                         && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))

                {
                    return Redirect(returnUrl);
                }
                else if (dataItem.Role == "Admin")
                {
                    return RedirectToAction("IndexAdmin");
                }
                else if (dataItem.Role == "Teacher")
                {
                    return RedirectToAction("IndexOgretmen");
                }
                else if (dataItem.Role == "Student")
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


        [Authorize]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}