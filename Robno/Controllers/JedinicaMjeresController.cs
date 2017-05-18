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
    public class JedinicaMjeresController : Controller
    {
        private RobnoContext db = new RobnoContext();

        // GET: JedinicaMjeres
        public ActionResult Index()
        {
            return View(db.JedinicaMjeres.ToList());
        }

        // GET: JedinicaMjeres/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JedinicaMjere jedinicaMjere = db.JedinicaMjeres.Find(id);
            if (jedinicaMjere == null)
            {
                return HttpNotFound();
            }
            return View(jedinicaMjere);
        }

        // GET: JedinicaMjeres/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: JedinicaMjeres/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [AdminFilter]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JedinicaMjereID,Naziv,Kratica")] JedinicaMjere jedinicaMjere)
        {
            if (ModelState.IsValid)
            {
                db.JedinicaMjeres.Add(jedinicaMjere);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jedinicaMjere);
        }

        // GET: JedinicaMjeres/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JedinicaMjere jedinicaMjere = db.JedinicaMjeres.Find(id);
            if (jedinicaMjere == null)
            {
                return HttpNotFound();
            }
            return View(jedinicaMjere);
        }

        // POST: JedinicaMjeres/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [AdminFilter]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JedinicaMjereID,Naziv,Kratica")] JedinicaMjere jedinicaMjere)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jedinicaMjere).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jedinicaMjere);
        }

        // GET: JedinicaMjeres/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JedinicaMjere jedinicaMjere = db.JedinicaMjeres.Find(id);
            if (jedinicaMjere == null)
            {
                return HttpNotFound();
            }
            return View(jedinicaMjere);
        }

        // POST: JedinicaMjeres/Delete/5
        [AdminFilter]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JedinicaMjere jedinicaMjere = db.JedinicaMjeres.Find(id);
            db.JedinicaMjeres.Remove(jedinicaMjere);
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
