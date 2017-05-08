using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Projesi.Models;

namespace Web_Projesi.Controllers
{
    public class LoggedDanismanController : Controller
    {
        [Authorize(Roles = "Danisman")]
        // GET: LoggedDanisman
        public ActionResult Index()
        {
            return View();
        }
     
    }
}