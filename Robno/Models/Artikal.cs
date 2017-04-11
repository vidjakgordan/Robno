using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Robno.Models
{
    public class Artikal
    {
        public int ArtikalID { get; set; }

        public string Naziv { get; set; }
        public string Opis { get; set; }
        public string BarCode { get; set; }
        public string DodSifr { get; set; }
        public double Tezina { get; set; } // ovo samo nasilu broj u KG
        public double NabCijena { get; set; }
        public double ProdCijena { get; set; }
        public double Kolicina { get; set; }

        public virtual JedMje JedMje { get; set; }
        public virtual ArtKls ArtKls { get; set; }
        public virtual Tarifa Tarifa { get; set; }

        public virtual ICollection<PrimArt> PrimArts { get; set; }
        public virtual ICollection<RacArt> RacArts { get; set; }


    }
}