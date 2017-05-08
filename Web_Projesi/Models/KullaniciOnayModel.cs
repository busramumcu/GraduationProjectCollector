using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web_Projesi.Models
{
    public class KullaniciOnayModel
    {
        public IList<int> SecilmisKullanicilar { get; set; }
        public IList<Bekleyen_Kullanici> TumKullanicilar { get; set; }

        public KullaniciOnayModel()
        {
            SecilmisKullanicilar = new List<int>();
            TumKullanicilar = new List<Bekleyen_Kullanici>();
        }
    }
}