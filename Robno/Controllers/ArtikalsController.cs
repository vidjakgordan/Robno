using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Robno.Models;
using PagedList;

namespace Robno.Controllers
{
    public class ArtikalsController : Controller
    {
        private RobnoContext db = new RobnoContext();

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.OrderParm = String.IsNullOrEmpty(sortOrder) ? "naziv_asc" : "";

            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;
            
            var artikals = from s in db.Artikals select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                artikals = artikals.Where(s => s.Naziv.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "naziv_asc":
                    artikals = artikals.OrderBy(s => s.Naziv);
                    break;
                default:
                    artikals = artikals.OrderBy(s => s.ArtikalID);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(artikals.ToPagedList(pageNumber, pageSize));
        }


        // GET: Artikals/Details/5
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
            return View(artikal);
        }

        // GET: Artikals/Create
        public ActionResult Create()
        {
            ViewBag.ArtikalKlasaID = new SelectList(db.ArtikalKlasas, "ArtikalKlasaID", "Naziv");
            ViewBag.JedinicaMjereID = new SelectList(db.JedinicaMjeres, "JedinicaMjereID", "Naziv");
            ViewBag.TarifaID = new SelectList(db.Tarifas, "TarifaID", "Opis");
            return View();
        }

        // POST: Artikals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArtikalID,Naziv,Opis,BarCode,DodatnaSifra,Tezina,NabavnaCijena,ProdajnaCijena,Kolicina,JedinicaMjereID,ArtikalKlasaID,TarifaID")] Artikal artikal)
        {
            if (ModelState.IsValid)
            {
                db.Artikals.Add(artikal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArtikalKlasaID = new SelectList(db.ArtikalKlasas, "ArtikalKlasaID", "Naziv", artikal.ArtikalKlasaID);
            ViewBag.JedinicaMjereID = new SelectList(db.JedinicaMjeres, "JedinicaMjereID", "Naziv", artikal.JedinicaMjereID);
            ViewBag.TarifaID = new SelectList(db.Tarifas, "TarifaID", "Opis", artikal.TarifaID);
            return View(artikal);
        }

        // GET: Artikals/Edit/5
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
            ViewBag.ArtikalKlasaID = new SelectList(db.ArtikalKlasas, "ArtikalKlasaID", "Naziv", artikal.ArtikalKlasaID);
            ViewBag.JedinicaMjereID = new SelectList(db.JedinicaMjeres, "JedinicaMjereID", "Naziv", artikal.JedinicaMjereID);
            ViewBag.TarifaID = new SelectList(db.Tarifas, "TarifaID", "Opis", artikal.TarifaID);
            return View(artikal);
        }

        // POST: Artikals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArtikalID,Naziv,Opis,BarCode,DodatnaSifra,Tezina,NabavnaCijena,ProdajnaCijena,Kolicina,JedinicaMjereID,ArtikalKlasaID,TarifaID")] Artikal artikal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artikal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArtikalKlasaID = new SelectList(db.ArtikalKlasas, "ArtikalKlasaID", "Naziv", artikal.ArtikalKlasaID);
            ViewBag.JedinicaMjereID = new SelectList(db.JedinicaMjeres, "JedinicaMjereID", "Naziv", artikal.JedinicaMjereID);
            ViewBag.TarifaID = new SelectList(db.Tarifas, "TarifaID", "Opis", artikal.TarifaID);
            return View(artikal);
        }

        // GET: Artikals/Delete/5
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

        // POST: Artikals/Delete/5
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
