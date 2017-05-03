using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Robno.Models
{
    //kreirano za prikaz svih stavki koje pripadaju nekoj primci!!!
    public class PrimkaStavkeVM
    {
        public Primka Primka { get; set; }
        public IEnumerable<StavkaPrimke> Stavke { get; set; }
    }
}