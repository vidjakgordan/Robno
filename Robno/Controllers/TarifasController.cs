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
    public class TarifasController : Controller
    {
        
        private RobnoContext db = new RobnoContext();

        // GET: Tarifas
        public ActionResult Index()
        {
            return View(db.Tarifas.ToList());
        }

        // GET: Tarifas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tarifa tarifa = db.Tarifas.Find(id);
            if (tarifa == null)
            {
                return HttpNotFound();
            }
            return View(tarifa);
        }

        // GET: Tarifas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tarifas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TarifaID,Opis,Stopa")] Tarifa tarifa)
        {
            if (ModelState.IsValid)
            {
                db.Tarifas.Add(tarifa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tarifa);
        }

        // GET: Tarifas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tarifa tarifa = db.Tarifas.Find(id);
            if (tarifa == null)
            {
                return HttpNotFound();
            }
            return View(tarifa);
        }

        // POST: Tarifas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TarifaID,Opis,Stopa")] Tarifa tarifa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tarifa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tarifa);
        }

        // GET: Tarifas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tarifa tarifa = db.Tarifas.Find(id);
            if (tarifa == null)
            {
                return HttpNotFound();
            }
            return View(tarifa);
        }

        // POST: Tarifas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tarifa tarifa = db.Tarifas.Find(id);
            db.Tarifas.Remove(tarifa);
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
