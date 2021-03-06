﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Robno.Models;
using PagedList;
using Robno.Filters;

namespace Robno.Controllers
{
    [Authorize]
    public class PoslovniPartnersController : Controller
    {
        private RobnoContext db = new RobnoContext();

        // GET: PoslovniPartners
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            //return View(db.PoslovniPartners.ToList());

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NazivSortParm = String.IsNullOrEmpty(sortOrder) ? "naziv_desc" : "";
            ViewBag.MjestoSortParm = sortOrder == "Mjesto" ? "mjesto_desc" : "Mjesto";

            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;

            var poslovnipartneri = from s in db.PoslovniPartners select s;

            if(!String.IsNullOrEmpty(searchString))
            {
                poslovnipartneri = poslovnipartneri.Where(s => s.Naziv.Contains(searchString));
            }
            
            switch(sortOrder)
            {
                case "naziv_desc":
                    poslovnipartneri = poslovnipartneri.OrderByDescending(s => s.Naziv);
                    break;
                case "Mjesto":
                    poslovnipartneri = poslovnipartneri.OrderBy(s => s.Mjesto);
                    break;
                case "mjesto_desc":
                    poslovnipartneri = poslovnipartneri.OrderByDescending(s => s.Mjesto);
                    break;
                default:
                    poslovnipartneri = poslovnipartneri.OrderBy(s => s.PoslovniPartnerID);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(poslovnipartneri.ToPagedList(pageNumber, pageSize));
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
        [AdminFilter]
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
        [AdminFilter]
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
        [AdminFilter]
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
