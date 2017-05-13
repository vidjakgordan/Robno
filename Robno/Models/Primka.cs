using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Robno.Models
{
    public class Primka
    {
        public int PrimkaID { get; set; }

        [DisplayName("Datum izdavanja")]
        public DateTime? DatumIzdavanja { get; set; }
        [DisplayName("Datum unosa")]
        public DateTime? DatumUnosa { get; set; }
        [DisplayName("Tečaj valute")]
        public double? ValutaTecaj { get; set; }
        [DisplayName("Ukupni iznos")]
        public double? UkupniIznos { get; set; }
        public string Napomena { get; set; }
        public int? Status { get; set; } // za kasnije

        public int? PoslovniPartnerID { get; set; }
        public int? ValutaID { get; set; }

        [DisplayName("Poslovni partner")]
        [ForeignKey("PoslovniPartnerID")]
        public virtual PoslovniPartner PoslovniPartner { get; set; }

        [ForeignKey("ValutaID")]
        public virtual Valuta Valuta { get; set; }

        public virtual ICollection<StavkaPrimke> StavkaPrimkes { get; set; }
    }
}