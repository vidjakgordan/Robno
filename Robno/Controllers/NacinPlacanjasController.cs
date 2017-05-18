using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Robno.Models;
using Robno.Filters;

namespace Robno.Controllers
{
    [Authorize]
    public class NacinPlacanjasController : Controller
    {
        private RobnoContext db = new RobnoContext();

        // GET: NacinPlacanjas
        public ActionResult Index()
        {
            return View(db.NacinPlacanjas.ToList());
        }

        // GET: NacinPlacanjas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NacinPlacanja nacinPlacanja = db.NacinPlacanjas.Find(id);
            if (nacinPlacanja == null)
            {
                return HttpNotFound();
            }
            return View(nacinPlacanja);
        }

        // GET: NacinPlacanjas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NacinPlacanjas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [AdminFilter]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NacinPlacanjaID,Naziv,Kratica")] NacinPlacanja nacinPlacanja)
        {
            if (ModelState.IsValid)
            {
                db.NacinPlacanjas.Add(nacinPlacanja);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nacinPlacanja);
        }

        // GET: NacinPlacanjas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NacinPlacanja nacinPlacanja = db.NacinPlacanjas.Find(id);
            if (nacinPlacanja == null)
            {
                return HttpNotFound();
            }
            return View(nacinPlacanja);
        }

        // POST: NacinPlacanjas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [AdminFilter]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NacinPlacanjaID,Naziv,Kratica")] NacinPlacanja nacinPlacanja)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nacinPlacanja).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nacinPlacanja);
        }

        // GET: NacinPlacanjas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NacinPlacanja nacinPlacanja = db.NacinPlacanjas.Find(id);
            if (nacinPlacanja == null)
            {
                return HttpNotFound();
            }
            return View(nacinPlacanja);
        }

        // POST: NacinPlacanjas/Delete/5
        [AdminFilter]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NacinPlacanja nacinPlacanja = db.NacinPlacanjas.Find(id);
            db.NacinPlacanjas.Remove(nacinPlacanja);
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
