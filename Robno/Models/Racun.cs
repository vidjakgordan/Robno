using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Robno.Models
{
    public class Racun
    {
        public int RacunID { get; set; }

        public DateTime DatumIzd { get; set; }
        public string Napomena { get; set; }
        public string ZKI { get; set; }
        public string JIR { get; set; }
        public double UkIznos { get; set; }
        public int Status { get; set; } // za nesto kasnije
        
        public virtual PoslPart PoslPart { get; set; }
        public virtual SifPlac SifPlac { get; set; }
        
        public virtual ICollection<RacArt> RacArts { get; set; }
    }
}