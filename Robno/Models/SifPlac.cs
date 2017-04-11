using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Robno.Models
{
    public class SifPlac
    {
        public int SifPlacID { get; set;}
        public string Naziv { get; set; }
        public string Kratica { get; set; }

        public virtual ICollection<Racun> Racuns { get; set; }
    }
}