using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Projesi.Models
{
    public class DanismanAtamaModel
    {
        public List<Kullanici> ogrenciler;
        public List<Kullanici> danismanlar;
        public int Kullanici_Id { get; set; }
        public int secilenDanismanId { get; set; }
        public DanismanAtamaModel()
        {
            ogrenciler = new List<Kullanici>();
            danismanlar = new List<Kullanici>();
        }

    }
}