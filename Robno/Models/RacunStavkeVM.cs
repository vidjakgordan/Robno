using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Robno.Models
{

    //kreirano za prikaz svih stavki koje pripadaju jednom racunu!!
    public class RacunStavkeVM
    {
        public Racun Racun { get; set; }
        public IEnumerable<StavkaRacuna> Stavke { get; set; }
    }
}