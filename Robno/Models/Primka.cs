using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Robno.Models
{
    public class Primka
    {
        public int PrimkaID { get; set; }

        public DateTime DatumIzd { get; set; }
        public DateTime DatumUnos { get; set; }
        public double ValutaTecaj { get; set; }
        public double UkIznos { get; set; }
        public string Napomena { get; set; }
        public int Status { get; set; } // za kasnije

        public virtual PoslPart PoslPart { get; set; }
        public virtual Valuta Valuta { get; set; }

        public virtual ICollection<PrimArt> PrimArts { get; set; }
    }
}