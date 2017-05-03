﻿using System;
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
    public class RacunsController : Controller
    {
        private RobnoContext db = new RobnoContext();

        // GET: Racuns
        public ActionResult Index()
        {
            return View(db.Racuns.ToList());
        }

        // GET: Racuns/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Racun racun = db.Racuns.Find(id);
            Racun racun = db.Racuns.Include("NacinPlacanja").Include("PoslovniPartner").SingleOrDefault(x => x.RacunID == id);
                        
            IEnumerable<StavkaRacuna> stavke = db.StavkaRacunas.Include("Artikal").Where(x => x.Racun.RacunID == id);

            RacunStavkeVM vm = new RacunStavkeVM();
            vm.Racun = racun;
            vm.Stavke = stavke;
           
            if (racun == null)
            {
                return HttpNotFound();
            }
            return View(vm);
        }

        // GET: Racuns/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Racuns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RacunID,DatumIzdavanja,Napomena,ZKI,JIR,UkupniIznos,Status")] Racun racun)
        {
            if (ModelState.IsValid)
            {
                db.Racuns.Add(racun);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(racun);
        }

        // GET: Racuns/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Racun racun = db.Racuns.Find(id);
            if (racun == null)
            {
                return HttpNotFound();
            }
            return View(racun);
        }

        // POST: Racuns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RacunID,DatumIzdavanja,Napomena,ZKI,JIR,UkupniIznos,Status")] Racun racun)
        {
            if (ModelState.IsValid)
            {
                db.Entry(racun).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(racun);
        }

        // GET: Racuns/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Racun racun = db.Racuns.Find(id);
            if (racun == null)
            {
                return HttpNotFound();
            }
            return View(racun);
        }

        // POST: Racuns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Racun racun = db.Racuns.Find(id);
            db.Racuns.Remove(racun);
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