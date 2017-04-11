using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Robno.Models
{
    public class PrimArt
    {
        // IMAN VEZU PREMA Primka i prema Artikal - kako to napravit - primary key koji je od 2 sastavljen?!

        public double Kolicina { get; set; }
        public double NabCijena { get; set; }
        public double Rabat { get; set; }
    }
}