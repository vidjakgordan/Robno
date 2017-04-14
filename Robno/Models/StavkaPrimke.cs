using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Robno.Models
{
    public class StavkaPrimke
    {
        public int StavkaPrimkeID { get; set; }

        public int? RedniBrojStavke { get; set; }
        public double? Kolicina { get; set; }
        public double? NabavnaCijena { get; set; }
        public double? Rabat { get; set; }

        public virtual Primka Primka { get; set; }
        public virtual Artikal Artikal { get; set; }
    }
}