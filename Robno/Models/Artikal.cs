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
        public string DodatnaSifra { get; set; }
        public double? Tezina { get; set; } // ovo samo nasilu broj u KG
        public double? NabavnaCijena { get; set; }
        public double? ProdajnaCijena { get; set; }
        public double? Kolicina { get; set; }

        public virtual JedinicaMjere JedinicaMjere { get; set; }
        public virtual ArtikalKlasa ArtikalKlasa { get; set; }
        public virtual Tarifa Tarifa { get; set; }

        public virtual ICollection<StavkaPrimke> StavkaPrimkes { get; set; }
        public virtual ICollection<StavkaRacuna> StavkaRacunas { get; set; }


    }
}