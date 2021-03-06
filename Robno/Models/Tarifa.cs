﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Robno.Models
{
    public class Tarifa
    {
        public int TarifaID { get; set; }

        [DisplayName("Porezna tarifa")]
        public string Opis { get; set; }
        public double? Stopa { get; set; }

        public virtual ICollection<Artikal> Artikals { get; set;}
        public virtual ICollection<StavkaRacuna> StavkaRacunas { get; set; }
    }
}