using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Robno.Models;

namespace Robno.Controllers
{
    public class ArtikalsController : Controller
    {
        private RobnoContext db = new RobnoContext();

        // GET: /Artikals/
        public ActionResult Index()
        {
            var articals = db.Artikals
                .Include("Tarifa")
                .Include("JedinicaMjere")
                .Include("ArtikalKlasa");

            return View(articals.ToList());
        }

        // GET: /Artikals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artikal artikal = db.Artikals.Find(id);
            if (artikal == null)
            {
                return HttpNotFound();
            }

            var articals = db.Artikals
                .Include("Tarifa")
                .Include("JedinicaMjere")
                .Include("ArtikalKlasa")
                .ToList()
                .SingleOrDefault(x => x.ArtikalID == id);

            return View(articals);
        }

        // GET: /Artikals/Create  //ovo radi
        public ActionResult Create()
        {
            var model = new ArtikalViewModel();
            return View(model);
        }

        // POST: /Artikals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ArtikalID,Naziv,Opis,BarCode,DodatnaSifra,Tezina,NabavnaCijena,ProdajnaCijena,Kolicina,TarifaID,JedinicaMjereID,ArtikalKlasaID")] Artikal artikal)
        {
            if (ModelState.IsValid)
            {
                db.Artikals.Add(artikal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(artikal);
        }

        // GET: /Artikals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artikal artikal = db.Artikals.Find(id);
            if (artikal == null)
            {
                return HttpNotFound();
            }

            var model = new ArtikalViewModel() { Artikal = artikal };
            return View(model);
        }

        // POST: /Artikals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ArtikalID,Naziv,Opis,BarCode,DodatnaSifra,Tezina,NabavnaCijena,ProdajnaCijena,Kolicina,TarifaID, JedinicaMjereID, ArtikalKlasaID")] Artikal artikal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artikal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(artikal);
        }

        // GET: /Artikals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artikal artikal = db.Artikals.Find(id);
            if (artikal == null)
            {
                return HttpNotFound();
            }
            return View(artikal);
        }

        // POST: /Artikals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Artikal artikal = db.Artikals.Find(id);
            db.Artikals.Remove(artikal);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
