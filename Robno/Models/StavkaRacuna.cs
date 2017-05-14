using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Robno.Models
{
    public class StavkaRacuna
    {
        public int StavkaRacunaID { get; set; }

        public int? RedniBrojStavke { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.00}")]
        public double? Kolicina { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.00}")]
        public double? NabavnaCijena { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.00}")]
        public double? ProdajnaCijena { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.00}")]
        public double? Popust { get; set; }
        
        public virtual Tarifa Tarifa { get; set; }
        
        public virtual Artikal Artikal { get; set; }
        public virtual Racun Racun { get; set; }
    }
}