using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Projesi.Models
{
    public class GorevAyrintiModel
    {
        public List<Kullanici> dosyaYukleyenKullanicilar;
        public int Gorev_Id;
        public GorevAyrintiModel()
        {
            dosyaYukleyenKullanicilar = new List<Kullanici>();
        }
    }
}