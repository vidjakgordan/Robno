using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
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
        [DisplayName("Sifra")]
        public string DodatnaSifra { get; set; }
        public double? Tezina { get; set; } // ovo samo nasilu broj u KG
        [DisplayName("Nabavna cijena")]
        public double? NabavnaCijena { get; set; }
        [DisplayName("Prodajna cijena")]
        public double? ProdajnaCijena { get; set; }
        public double? Kolicina { get; set; }

        public int? JedinicaMjereID { get; set; }
        [ForeignKey("JedinicaMjereID")]
        public virtual JedinicaMjere JedinicaMjere { get; set; }

        public int? ArtikalKlasaID { get; set; }
        [ForeignKey("ArtikalKlasaID")]
        public virtual ArtikalKlasa ArtikalKlasa { get; set; }

        public int TarifaID { get; set; }
        [ForeignKey("TarifaID")]
        public virtual Tarifa Tarifa { get; set; }

        public virtual ICollection<StavkaPrimke> StavkaPrimkes { get; set; }
        public virtual ICollection<StavkaRacuna> StavkaRacunas { get; set; }


    }
}