using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Robno.Models;

namespace Robno.Controllers
{
    public class ArtikalController : ApiController
    {
        private RobnoContext db = new RobnoContext();

        public class ArtikalDB2V
        {
            public int Id { get; private set; }
            public string Naziv { get; private set; }
            public string Barcode { get; private set; }
            public double? ProdajnaCijena { get; private set; }
            public double? Kolicina { get; private set; }
            public int TarifaID { get; set; }

            public ArtikalDB2V(int id, string naziv, string barcode, double? prodajnacijena, double? kolicina, int tarifaid)
            {
                Id = id;
                Naziv = naziv;
                Barcode = barcode;
                ProdajnaCijena = prodajnacijena;
                Kolicina = kolicina;
                TarifaID = tarifaid;
            }
        }

        public class ArtikalDTO
        {
            public int ProductId { get; set; }
            public int ProductPopust { get; set; }
            public int ProductProdajnaCijena { get; set; }
            public string ProductKolicina { get; set; }
            public int ProductTarifa { get; set; }
         }

       /* public class ArtikalDTO2DB
        {
            public double ProductPopust { get; set; }
            public double ProductProdajnaCijena { get; set; }
            public double ProductKolicina { get; set; }
            public double ProductTarifa { get; set; }

            public ArtikalDTO2DB(ArtikalDTO artikal)
            {
                ProductId = artikal.ProductId;
                ProductPopust = artikal.ProductPopust;
                ProductProdajnaCijena = artikal.ProductProdajnaCijena;
                ProductKolicina = Convert.ToDouble(artikal.ProductKolicina);
                ProductTarifa = artikal.ProductTarifa;
            }
        }*/

        public abstract class Mapper
        {
            public static ArtikalDB2V Map(Artikal artikal)
            {
                return new ArtikalDB2V(
                    id: artikal.ArtikalID,
                    naziv: artikal.Naziv,
                    barcode: artikal.BarCode,
                    prodajnacijena: artikal.ProdajnaCijena,
                    kolicina: artikal.Kolicina,
                    tarifaid: artikal.Tarifa.TarifaID
                );
            }
        }
        // GET api/Artikal
        public IQueryable<ArtikalDB2V> GetArtikals()
        {
            var x = 
                db.Artikals
                .Select(Mapper.Map)
                .AsQueryable();
            return x;
        }

        // GET api/Artikal/5
        [ResponseType(typeof(Artikal))]
        public IHttpActionResult GetArtikal(int id)
        {
            Artikal artikal = db.Artikals.Find(id);
            if (artikal == null)
            {
                return NotFound();
            }

            return Ok(artikal);
        }

        // PUT api/Artikal/5
        public IHttpActionResult PutArtikal(int id, Artikal artikal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != artikal.ArtikalID)
            {
                return BadRequest();
            }

            db.Entry(artikal).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtikalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Artikal
        [ResponseType(typeof(Artikal))]
        public IHttpActionResult PostArtikal(IEnumerable<ArtikalDTO> stavke)
        {
            var racun = db.Racuns.Create();
            racun.DatumIzdavanja = DateTime.Now;

            int i = 1;
            foreach(var stavka in stavke)
            {
                var novaStavka = db.StavkaRacunas.Create();

                novaStavka.RedniBrojStavke = i;
                i++;
                novaStavka.Racun = racun;
                novaStavka.Artikal = db.Artikals.Find(stavka.ProductId);

                novaStavka.Popust = stavka.ProductPopust;
                novaStavka.Kolicina = Convert.ToDouble(stavka.ProductKolicina);
                novaStavka.ProdajnaCijena = stavka.ProductProdajnaCijena;
                novaStavka.NabavnaCijena = db.Artikals.Find(stavka.ProductId).NabavnaCijena;
                db.StavkaRacunas.Add(novaStavka);
            }

            db.Racuns.Add(racun);
            db.SaveChanges();

            return Ok();
        }

        // DELETE api/Artikal/5
        [ResponseType(typeof(Artikal))]
        public IHttpActionResult DeleteArtikal(int id)
        {
            Artikal artikal = db.Artikals.Find(id);
            if (artikal == null)
            {
                return NotFound();
            }

            db.Artikals.Remove(artikal);
            db.SaveChanges();

            return Ok(artikal);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ArtikalExists(int id)
        {
            return db.Artikals.Count(e => e.ArtikalID == id) > 0;
        }
    }
}