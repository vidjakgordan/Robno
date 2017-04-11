using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Robno.Models
{
    public class Valuta
    {
        public int ValutaID { get; set; }
        
        public string Naziv { get; set; }
        public string Kratica { get; set; }

        public virtual ICollection<Primka> Primkas { get; set; }
    }
}