using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Robno.Models
{
    public class Primka
    {
        public int PrimkaID { get; set; }

        public DateTime? DatumIzdavanja { get; set; }
        public DateTime? DatumUnosa { get; set; }
        public double? ValutaTecaj { get; set; }
        public double? UkupniIznos { get; set; }
        public string Napomena { get; set; }
        public int? Status { get; set; } // za kasnije

        public virtual PoslovniPartner PoslovniPartner { get; set; }
        public virtual Valuta Valuta { get; set; }

        public virtual ICollection<StavkaPrimke> StavkaPrimkes { get; set; }
    }
}