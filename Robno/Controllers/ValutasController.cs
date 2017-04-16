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
    public class ValutasController : Controller
    {
        private RobnoContext db = new RobnoContext();

        // GET: Valutas
        public ActionResult Index()
        {
            return View(db.Valutas.ToList());
        }

        // GET: Valutas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Valuta valuta = db.Valutas.Find(id);
            if (valuta == null)
            {
                return HttpNotFound();
            }
            return View(valuta);
        }

        // GET: Valutas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Valutas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ValutaID,Naziv,Kratica")] Valuta valuta)
        {
            if (ModelState.IsValid)
            {
                db.Valutas.Add(valuta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(valuta);
        }

        // GET: Valutas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Valuta valuta = db.Valutas.Find(id);
            if (valuta == null)
            {
                return HttpNotFound();
            }
            return View(valuta);
        }

        // POST: Valutas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ValutaID,Naziv,Kratica")] Valuta valuta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(valuta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(valuta);
        }

        // GET: Valutas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Valuta valuta = db.Valutas.Find(id);
            if (valuta == null)
            {
                return HttpNotFound();
            }
            return View(valuta);
        }

        // POST: Valutas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Valuta valuta = db.Valutas.Find(id);
            db.Valutas.Remove(valuta);
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
