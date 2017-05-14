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
using Rotativa;

namespace Robno.Controllers
{
    public class PrimkasController : Controller
    {
        private RobnoContext db = new RobnoContext();

        // GET: Primkas
        public ActionResult Index(int? page)
        {
            var primkas = db.Primkas.Include(p => p.PoslovniPartner).
                Include(p => p.Valuta).OrderByDescending(p=> p.PrimkaID);

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(primkas.ToPagedList(pageNumber, pageSize));
        }

        // GET: Primkas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Primka primka = db.Primkas.Find(id);
            if (primka == null)
            {
                return HttpNotFound();
            }
            return View(primka);
        }

        //rotativa - print to pdf
        public ActionResult PrintDetails(int id)
        {
            string filename = "Primka_" + id + ".pdf";
            return new ActionAsPdf(
                           "Details",
                           new { id = id })
            { FileName = filename };
        }

        // GET: Primkas/Create
        public ActionResult Create()
        {
            ViewBag.PoslovniPartnerID = new SelectList(db.PoslovniPartners, "PoslovniPartnerID", "Naziv");
            ViewBag.ValutaID = new SelectList(db.Valutas, "ValutaID", "Naziv");
            return View();
        }

        // POST: Primkas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PrimkaID,DatumIzdavanja,DatumUnosa,ValutaTecaj,UkupniIznos,Napomena,Status,PoslovniPartnerID,ValutaID")] Primka primka)
        {
            if (ModelState.IsValid)
            {
                db.Primkas.Add(primka);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PoslovniPartnerID = new SelectList(db.PoslovniPartners, "PoslovniPartnerID", "Naziv", primka.PoslovniPartnerID);
            ViewBag.ValutaID = new SelectList(db.Valutas, "ValutaID", "Naziv", primka.ValutaID);
            return View(primka);
        }

        // GET: Primkas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Primka primka = db.Primkas.Find(id);
            if (primka == null)
            {
                return HttpNotFound();
            }
            ViewBag.PoslovniPartnerID = new SelectList(db.PoslovniPartners, "PoslovniPartnerID", "Naziv", primka.PoslovniPartnerID);
            ViewBag.ValutaID = new SelectList(db.Valutas, "ValutaID", "Naziv", primka.ValutaID);
            return View(primka);
        }

        // POST: Primkas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PrimkaID,DatumIzdavanja,DatumUnosa,ValutaTecaj,UkupniIznos,Napomena,Status,PoslovniPartnerID,ValutaID")] Primka primka)
        {
            if (ModelState.IsValid)
            {
                db.Entry(primka).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PoslovniPartnerID = new SelectList(db.PoslovniPartners, "PoslovniPartnerID", "Naziv", primka.PoslovniPartnerID);
            ViewBag.ValutaID = new SelectList(db.Valutas, "ValutaID", "Naziv", primka.ValutaID);
            return View(primka);
        }

        // GET: Primkas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Primka primka = db.Primkas.Find(id);
            if (primka == null)
            {
                return HttpNotFound();
            }
            return View(primka);
        }

        // POST: Primkas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Primka primka = db.Primkas.Find(id);
            db.Primkas.Remove(primka);
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
