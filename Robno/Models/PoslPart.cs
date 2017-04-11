using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Robno.Models
{
    public class PoslPart
    {
        public int PoslPartID { get; set; }

        public string Naziv { get; set;}
        public string Adresa { get; set; }
        public string Mjesto { get; set; }
        public string OIB { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string Mail { get; set; }
        public string WWW { get; set; }
        public string Ziro1 { get; set; }
        public string Ziro2 { get; set; }
        public string Napomena { get; set; }

        public virtual ICollection<Racun> Racuns { get; set; }
        public virtual ICollection<Primka> Primkas { get; set; }

    }
}