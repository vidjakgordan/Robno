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
        public SelectList JediniceMjere { get; set; }
        public SelectList ArtikalKlase { get; set; }

        public ArtikalViewModel()
        {
            var dbContext = new RobnoContext();
            Tarife = new SelectList(dbContext.Tarifas.ToList(), "TarifaID", "Opis");
            JediniceMjere = new SelectList(dbContext.JedinicaMjeres.ToList(), "JedinicaMjereID", "Naziv");
            ArtikalKlase = new SelectList(dbContext.ArtikalKlasas.ToList(), "ArtikalKlasaID", "Naziv");
        }
    }
}                              