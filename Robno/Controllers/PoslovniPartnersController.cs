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
    public class PoslovniPartnersController : Controller
    {
        private RobnoContext db = new RobnoContext();

        // GET: PoslovniPartners
        public ActionResult Index()
        {
            return View(db.PoslovniPartners.ToList());
        }

        // GET: PoslovniPartners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoslovniPartner poslovniPartner = db.PoslovniPartners.Find(id);
            if (poslovniPartner == null)
            {
                return HttpNotFound();
            }
            return View(poslovniPartner);
        }

        // GET: PoslovniPartners/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PoslovniPartners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PoslovniPartnerID,Naziv,Adresa,Mjesto,OIB,Tel,Fax,Mail,WWW,Ziro1,Ziro2,Napomena")] PoslovniPartner poslovniPartner)
        {
            if (ModelState.IsValid)
            {
                db.PoslovniPartners.Add(poslovniPartner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(poslovniPartner);
        }

        // GET: PoslovniPartners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoslovniPartner poslovniPartner = db.PoslovniPartners.Find(id);
            if (poslovniPartner == null)
            {
                return HttpNotFound();
            }
            return View(poslovniPartner);
        }

        // POST: PoslovniPartners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PoslovniPartnerID,Naziv,Adresa,Mjesto,OIB,Tel,Fax,Mail,WWW,Ziro1,Ziro2,Napomena")] PoslovniPartner poslovniPartner)
        {
            if (ModelState.IsValid)
            {
                db.Entry(poslovniPartner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(poslovniPartner);
        }

        // GET: PoslovniPartners/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoslovniPartner poslovniPartner = db.PoslovniPartners.Find(id);
            if (poslovniPartner == null)
            {
                return HttpNotFound();
            }
            return View(poslovniPartner);
        }

        // POST: PoslovniPartners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PoslovniPartner poslovniPartner = db.PoslovniPartners.Find(id);
            db.PoslovniPartners.Remove(poslovniPartner);
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
