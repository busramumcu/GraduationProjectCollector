using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web_Projesi.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TezKoordinatoru()
        {
            return View();
        }
        public ActionResult Danismanlar()
        {
            return View();
        }

        public ActionResult Iletisim()
        {
            return View();
        }

    }
}