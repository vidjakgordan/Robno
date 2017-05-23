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
using Robno.Filters;

namespace Robno.Controllers
{
    [System.Web.Http.Authorize]
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

        public class ArtikalDTORacun
        {
            public int ProductId { get; set; }
            public string ProductPopust { get; set; }
            public double ProductProdajnaCijena { get; set; }
            public string ProductKolicina { get; set; }
            public int ProductTarifa { get; set; }
         }

        public class ArtikalDTOPrimka
        {
            public int ProductId { get; set; }
            public string ProductRabat { get; set; }
            public double ProductNabavnaCijena { get; set; }
            public string ProductKolicina { get; set; }
        }

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
        //[ResponseType(typeof(Artikal))]
        [HttpPost]
        [Route("Racun")]
        public IHttpActionResult Racun(IEnumerable<ArtikalDTORacun> stavke) // za Racun
        {
            if (stavke.Count() == 0) return NotFound(); // ako su stavke prazne - ne moze bit racun bez stavki

            var racun = db.Racuns.Create();
            racun.DatumIzdavanja = DateTime.Now;
            double ukupniiznos = 0;

            int i = 1;
            foreach(var stavka in stavke)
            {
                //kreiranje nove stavke racune
                var novaStavka = db.StavkaRacunas.Create();

                novaStavka.RedniBrojStavke = i;
                i++;
                novaStavka.Racun = racun;
                novaStavka.Artikal = db.Artikals.Find(stavka.ProductId);
                novaStavka.Tarifa = db.Tarifas.Find(stavka.ProductTarifa);

                novaStavka.Popust = Convert.ToDouble(stavka.ProductPopust);
                novaStavka.Kolicina = Convert.ToDouble(stavka.ProductKolicina);
                novaStavka.ProdajnaCijena = stavka.ProductProdajnaCijena;
                novaStavka.NabavnaCijena = db.Artikals.Find(stavka.ProductId).NabavnaCijena;
                db.StavkaRacunas.Add(novaStavka);

                ukupniiznos = ukupniiznos + (double)novaStavka.Kolicina * (double)novaStavka.ProdajnaCijena * (1 - (double)novaStavka.Popust / 100);

                //skidanje sa stanja skladista
                db.Artikals.Find(stavka.ProductId).Kolicina = db.Artikals.Find(stavka.ProductId).Kolicina - novaStavka.Kolicina;
            }

            racun.UkupniIznos = ukupniiznos;
            racun.NacinPlacanja = db.NacinPlacanjas.Where(x => x.Kratica == "G").FirstOrDefault();

            db.Racuns.Add(racun);
            db.SaveChanges();

            return Ok();
        }

        // POST api/Artikal
        //[ResponseType(typeof(Artikal))]
        [AdminFilter]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("Primka")]
        public IHttpActionResult Primka(IEnumerable<ArtikalDTOPrimka> stavke) // za Primku
        {
            if (stavke.Count() == 0) return NotFound(); // ako su stavke prazne - ne moze bit primka bez stavki

            var primka = db.Primkas.Create();
            primka.DatumUnosa = DateTime.Now;
            double ukupniiznos = 0;

            int i = 1;
            foreach (var stavka in stavke)
            {
                //kreiranje nove stavke primke
                var novaStavka = db.StavkaPrimkes.Create();

                novaStavka.RedniBrojStavke = i;
                i++;
                novaStavka.Primka = primka;
                novaStavka.Artikal = db.Artikals.Find(stavka.ProductId);

                novaStavka.Rabat = Convert.ToDouble(stavka.ProductRabat);
                novaStavka.Kolicina = Convert.ToDouble(stavka.ProductKolicina);
                novaStavka.NabavnaCijena = stavka.ProductNabavnaCijena;
                db.StavkaPrimkes.Add(novaStavka);

                ukupniiznos += (double)novaStavka.NabavnaCijena * (double)novaStavka.Kolicina * (1 - (double)novaStavka.Rabat/100);

                //stavljanje sa stanja skladista
                
                double trenutnakolicina = (double)db.Artikals.Find(stavka.ProductId).Kolicina;
                double trenutnanabavnacijena = (double)db.Artikals.Find(stavka.ProductId).NabavnaCijena;

                db.Artikals.Find(stavka.ProductId).NabavnaCijena = 
                    (trenutnakolicina * trenutnanabavnacijena + novaStavka.Kolicina * novaStavka.NabavnaCijena) / 
                    (trenutnakolicina + novaStavka.Kolicina);

                db.Artikals.Find(stavka.ProductId).Kolicina = db.Artikals.Find(stavka.ProductId).Kolicina + novaStavka.Kolicina;
            }

            primka.UkupniIznos = ukupniiznos;

            db.Primkas.Add(primka);
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