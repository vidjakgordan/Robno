using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Robno.Models
{
    public class StavkaPrimke
    {
        public int StavkaPrimkeID { get; set; }

        public int? RedniBrojStavke { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.00}")]
        public double? Kolicina { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.00}")]
        public double? NabavnaCijena { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.00}")]
        public double? Rabat { get; set; }

        public virtual Primka Primka { get; set; }
        public virtual Artikal Artikal { get; set; }
    }
}