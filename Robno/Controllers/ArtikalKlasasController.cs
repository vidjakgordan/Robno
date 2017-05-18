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
    public class ArtikalKlasasController : Controller
    {
        private RobnoContext db = new RobnoContext();

        // GET: ArtikalKlasas
        public ActionResult Index()
        {
            return View(db.ArtikalKlasas.ToList());
        }

        // GET: ArtikalKlasas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtikalKlasa artikalKlasa = db.ArtikalKlasas.Find(id);
            if (artikalKlasa == null)
            {
                return HttpNotFound();
            }
            return View(artikalKlasa);
        }

        // GET: ArtikalKlasas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ArtikalKlasas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [AdminFilter]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArtikalKlasaID,Naziv,Kratica")] ArtikalKlasa artikalKlasa)
        {
            if (ModelState.IsValid)
            {
                db.ArtikalKlasas.Add(artikalKlasa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(artikalKlasa);
        }

        // GET: ArtikalKlasas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtikalKlasa artikalKlasa = db.ArtikalKlasas.Find(id);
            if (artikalKlasa == null)
            {
                return HttpNotFound();
            }
            return View(artikalKlasa);
        }

        // POST: ArtikalKlasas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [AdminFilter]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArtikalKlasaID,Naziv,Kratica")] ArtikalKlasa artikalKlasa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artikalKlasa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(artikalKlasa);
        }

        // GET: ArtikalKlasas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtikalKlasa artikalKlasa = db.ArtikalKlasas.Find(id);
            if (artikalKlasa == null)
            {
                return HttpNotFound();
            }
            return View(artikalKlasa);
        }

        // POST: ArtikalKlasas/Delete/5
        [AdminFilter]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ArtikalKlasa artikalKlasa = db.ArtikalKlasas.Find(id);
            db.ArtikalKlasas.Remove(artikalKlasa);
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
