using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Robno.Models
{
    public class StavkaRacuna
    {
        public int StavkaRacunaID { get; set; }

        public int RedniBrojStavke { get; set; }
        public double Kolicina { get; set; }
        public double NabavnaCijena { get; set; }
        public double ProdajnaCijena { get; set; }
        public double Popust { get; set; }
        
        public virtual Tarifa Tarifa { get; set; }
        
        public virtual Artikal Artikal { get; set; }
        public virtual Racun Racun { get; set; }
    }
}