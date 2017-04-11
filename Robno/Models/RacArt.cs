using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Robno.Models
{
    public class RacArt
    {

        //iman vezu prema Racun i vezu prema Artikal - primary key - kako slozit?!

        public double Kolicina { get; set; }
        public double NabCijena { get; set; }
        public double ProdCijena { get; set; }
        public double Popust { get; set; }
        
        public virtual Tarifa Tarifa { get; set; }
    }
}