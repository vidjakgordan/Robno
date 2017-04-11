using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Robno.Models
{
    public class JedMje
    {
        public int JedMjeID { get; set; }

        public string Naziv { get; set; }
        public string Kratica { get; set; }

        public virtual ICollection<Artikal> Artikals { get; set; }
    }
}