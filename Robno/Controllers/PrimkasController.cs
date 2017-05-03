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
    public class PrimkasController : Controller
    {
        private RobnoContext db = new RobnoContext();

        // GET: Primkas
        public ActionResult Index()
        {
            return View(db.Primkas.ToList());
        }

        // GET: Primkas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Primka primka = db.Primkas.Find(id);
            Primka primka = db.Primkas.Include("PoslovniPartner").Include("Valuta").SingleOrDefault(x => x.PrimkaID == id);
            IEnumerable<StavkaPrimke> stavke = db.StavkaPrimkes.Include("Artikal").Where(p => p.Primka.PrimkaID == id);

            PrimkaStavkeVM vm = new PrimkaStavkeVM();
            vm.Primka = primka;
            vm.Stavke = stavke;

            if (primka == null)
            {
                return HttpNotFound();
            }
            return View(vm);
        }

        // GET: Primkas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Primkas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PrimkaID,DatumIzdavanja,DatumUnosa,ValutaTecaj,UkupniIznos,Napomena,Status")] Primka primka)
        {
            if (ModelState.IsValid)
            {
                db.Primkas.Add(primka);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

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
            return View(primka);
        }

        // POST: Primkas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PrimkaID,DatumIzdavanja,DatumUnosa,ValutaTecaj,UkupniIznos,Napomena,Status")] Primka primka)
        {
            if (ModelState.IsValid)
            {
                db.Entry(primka).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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
