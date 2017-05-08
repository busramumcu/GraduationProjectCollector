using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web_Projesi.Controllers
{
    public class LoggedDanismanController : Controller
    {
        [Authorize(Roles = "Ogretim Uyesi")]
        // GET: LoggedDanisman
        public ActionResult Index()
        {
            return View();
        }
    }
}