using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Robno.Models
{
    public class Racun
    {
        public int RacunID { get; set; }

        public DateTime DatumIzdavanja { get; set; }
        public string Napomena { get; set; }
        public string ZKI { get; set; }
        public string JIR { get; set; }
        public double UkupniIznos { get; set; }
        public int Status { get; set; } // za nesto kasnije
        
        public virtual PoslovniPartner PoslovniPartner { get; set; }
        public virtual NacinPlacanja NacinPlacanja { get; set; }
        
        public virtual ICollection<StavkaRacuna> StavkaRacunas { get; set; }
    }
}