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

        [Authorize(Roles = "Danisman")]
        public ActionResult Gorev()
        {
            return View();
        }


        public JsonResult GetGorevs()
        {
            List<Gorev> all = null;

            using (TezProjectEntities db = new TezProjectEntities())
            {
                //    var contacts = (from a in dc.Contacts
                //                    join b in dc.Countries on a.CountryID equals b.CountryID
                //                    join c in dc.States on a.StateID equals c.StateID
                //                    select new
                //                    {
                //                        a,
                //                        b.CountryName,
                //                        c.StateName
                //                    });
                //    var gorevs = dc.Gorevs;
                //    if (gorevs != null)
                //    {
                //        all = new List<Gorev>();
                //        foreach (var i in contacts)
                //        {
                //            Contact con = i.a;
                //            con.CountryName = i.CountryName;
                //            con.StateName = i.StateName;
                //            all.Add(con);
                //        }
                //    }
                //}
                all = db.Gorevs.ToList();
                return new JsonResult { Data = all, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
    }
}