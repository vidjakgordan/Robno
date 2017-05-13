using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Robno.Models
{
    public class Racun
    {
        public int RacunID { get; set; }

        [DisplayName("Datum izdavanja")]
        public DateTime? DatumIzdavanja { get; set; }
        public string Napomena { get; set; }
        public string ZKI { get; set; }
        public string JIR { get; set; }
        [DisplayName("Ukupni iznos")]
        public double? UkupniIznos { get; set; }
        public int? Status { get; set; } // za nesto kasnije
        
        public int? PoslovniPartnerID { get; set; }
        public int? NacinPlacanjaID { get; set; }

        [DisplayName("Poslovni partner")]
        [ForeignKey("PoslovniPartnerID")]
        public virtual PoslovniPartner PoslovniPartner { get; set; }

        [DisplayName("Nacin placanja")]
        [ForeignKey("NacinPlacanjaID")]
        public virtual NacinPlacanja NacinPlacanja { get; set; }
        
        public virtual ICollection<StavkaRacuna> StavkaRacunas { get; set; }
    }
}