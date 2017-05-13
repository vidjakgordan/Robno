using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Robno.Models
{
    public class JedinicaMjere
    {
        public int JedinicaMjereID { get; set; }

        [DisplayName("Jedinica mjere")]
        public string Naziv { get; set; }
        public string Kratica { get; set; }

        public virtual ICollection<Artikal> Artikals { get; set; }
    }
}