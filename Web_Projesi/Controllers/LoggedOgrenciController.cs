using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        // GET: LoggedOgrenci
        public ActionResult BilgiDuzenle()
        {
            return View();
        }
    }
}