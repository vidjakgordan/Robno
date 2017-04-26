using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Robno.Models
{
    public class ArtikalViewModel
    {
        public Artikal Artikal { get; set; }
        public SelectList Tarife { get; set; }

        public ArtikalViewModel()
        {
            var dbContext = new RobnoContext();
            Tarife = new SelectList(dbContext.Tarifas.ToList(), "TarifaID", "Opis");
        }
    }
}                              