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

            public ArtikalDB2V(int id, string naziv, string barcode)
            {
                Id = id;
                Naziv = naziv;
                Barcode = barcode;
            }
        }

        public abstract class Mapper
        {
            public static ArtikalDB2V Map(Artikal artikal)
            {
                return new ArtikalDB2V(
                    id: artikal.ArtikalID,
                    naziv: artikal.Naziv,
                    barcode: artikal.BarCode
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
        public IHttpActionResult PostArtikal(Artikal artikal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Artikals.Add(artikal);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = artikal.ArtikalID }, artikal);
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