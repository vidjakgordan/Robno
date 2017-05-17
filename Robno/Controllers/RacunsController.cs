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
using Robno.Filters;

namespace Robno.Controllers
{
    [AdminFilter]
    public class RacunsController : Controller
    {
        private RobnoContext db = new RobnoContext();

        // GET: Racuns
        public ActionResult Index(int? page)
        {
            var racuns = db.Racuns.Include(r => r.NacinPlacanja)
                .Include(r => r.PoslovniPartner).OrderByDescending (r => r.RacunID);
            // dodano
            ViewBag.PoslovniPartnerID = new SelectList(db.PoslovniPartners, "PoslovniPartnerID", "Naziv");

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(racuns.ToPagedList(pageNumber, pageSize));
        }

        // GET: Racuns/Details/5
        public ActionResult Details(int? id)
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

            List<KeyValuePair<double, double>> test = new List<KeyValuePair<double, double>>();
            //ubaceno za radit zbrojeve poreznih tarifa
            foreach (StavkaRacuna stavka in racun.StavkaRacunas)
            {
                test.Add(new KeyValuePair<double, double>((double)stavka.Tarifa.Stopa, (double)stavka.ProdajnaCijena * (double)stavka.Kolicina * (1 - (double)stavka.Popust / 100)));
            }
            var result = test.GroupBy(r => r.Key).Select(r => new KeyValuePair<double, double>(r.Key, r.Sum(p => p.Value))).ToList();
            ViewBag.Porezi = result;


            return View(racun);
        }

        
        public ActionResult Search(string dateFrom, string dateTo, string idFrom, string idTo, int? PoslovniPartnerID)
        {
            var racun = from s in db.Racuns select s;

            if (dateFrom != null)
            {
                DateTime dateF;
                bool result = DateTime.TryParse(dateFrom, out dateF);
                if (result)
                {
                    racun = racun.Where(r => r.DatumIzdavanja >= dateF.Date);
                    ViewBag.dateFrom = dateFrom;
                }
            }

            if (dateTo != null)
            {
                DateTime dateF;
                bool result = DateTime.TryParse(dateTo, out dateF);
                if (result)
                {
                    racun = racun.Where(r => r.DatumIzdavanja <= dateF.Date);
                    ViewBag.dateTo = dateTo;
                }
            }

            if (idFrom != null)
            {
                int idF;
                bool result = int.TryParse(idFrom, out idF);
                if(result)
                {
                    racun = racun.Where(r => r.RacunID >= idF);
                    ViewBag.idFrom = idFrom;
                }
            }

            if (idTo != null)
            {
                int idF;
                bool result = int.TryParse(idTo, out idF);
                if (result)
                {
                    racun = racun.Where(r => r.RacunID <= idF);
                    ViewBag.idTo = idTo;
                }
            }

            if(PoslovniPartnerID != null)
            {
                racun = racun.Where(r => r.PoslovniPartnerID == PoslovniPartnerID);
                ViewBag.PoslovniPartnerID = PoslovniPartnerID;
            }

            //obrada raznih podataka za detalje!:
            ViewBag.BrojRacuna = racun.Count();

            double bruto = 0, neto = 0;


            List<KeyValuePair<double, double>> porez = new List<KeyValuePair<double, double>>();
            List<KeyValuePair<string, double>> nacinplacanja = new List<KeyValuePair<string, double>>();

            foreach (Racun rac in racun)
            {
                double netoracun = 0;
                foreach (StavkaRacuna stavka in rac.StavkaRacunas)
                {
                    porez.Add(new KeyValuePair<double, double>((double)stavka.Tarifa.Stopa, (double)stavka.ProdajnaCijena * (double)stavka.Kolicina * (1 - (double)stavka.Popust / 100)));
                    var brutotemp = (double)stavka.ProdajnaCijena * (double)stavka.Kolicina;
                    netoracun += brutotemp * (1 - (double)stavka.Popust / 100);
                    bruto += brutotemp;
                }
                neto += netoracun;
                nacinplacanja.Add(new KeyValuePair<string, double>(rac.NacinPlacanja.Naziv, netoracun));
            }

            var porezi = porez.GroupBy(r => r.Key).Select(r => new KeyValuePair<double, double>(r.Key, r.Sum(p => p.Value))).ToList();
            var naciniplacanja = nacinplacanja.GroupBy(r => r.Key).Select(r => new KeyValuePair<string, double>(r.Key, r.Sum(p => p.Value))).ToList();

            ViewBag.Porezi = porezi;
            ViewBag.NaciniPlacanja = naciniplacanja;

            ViewBag.Bruto = bruto;
            ViewBag.Neto = neto;
            ViewBag.Popust = bruto - neto;



            return View(racun);
        }

        //rotativa - za printanje u pdf
        public ActionResult PrintDetails(int id)
        {
            string filename = "Racun_" + id + ".pdf";
            return new ActionAsPdf(
                           "Details",
                           new { id = id })
            { FileName = filename };
        }

        // GET: Racuns/Create
        /*public ActionResult Create()
        {
            ViewBag.NacinPlacanjaID = new SelectList(db.NacinPlacanjas, "NacinPlacanjaID", "Naziv");
            ViewBag.PoslovniPartnerID = new SelectList(db.PoslovniPartners, "PoslovniPartnerID", "Naziv");
            return View();
        }*/

        // POST: Racuns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RacunID,DatumIzdavanja,Napomena,ZKI,JIR,UkupniIznos,Status,PoslovniPartnerID,NacinPlacanjaID")] Racun racun)
        {
            if (ModelState.IsValid)
            {
                db.Racuns.Add(racun);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NacinPlacanjaID = new SelectList(db.NacinPlacanjas, "NacinPlacanjaID", "Naziv", racun.NacinPlacanjaID);
            ViewBag.PoslovniPartnerID = new SelectList(db.PoslovniPartners, "PoslovniPartnerID", "Naziv", racun.PoslovniPartnerID);
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
            ViewBag.NacinPlacanjaID = new SelectList(db.NacinPlacanjas, "NacinPlacanjaID", "Naziv", racun.NacinPlacanjaID);
            ViewBag.PoslovniPartnerID = new SelectList(db.PoslovniPartners, "PoslovniPartnerID", "Naziv", racun.PoslovniPartnerID);
            return View(racun);
        }

        // POST: Racuns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RacunID,DatumIzdavanja,Napomena,ZKI,JIR,UkupniIznos,Status,PoslovniPartnerID,NacinPlacanjaID")] Racun racun)
        {
            if (ModelState.IsValid)
            {
                db.Entry(racun).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NacinPlacanjaID = new SelectList(db.NacinPlacanjas, "NacinPlacanjaID", "Naziv", racun.NacinPlacanjaID);
            ViewBag.PoslovniPartnerID = new SelectList(db.PoslovniPartners, "PoslovniPartnerID", "Naziv", racun.PoslovniPartnerID);
            return View(racun);
        }

        // GET: Racuns/Delete/5
        /*public ActionResult Delete(int? id)
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
        }*/

        // POST: Racuns/Delete/5
        /*[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Racun racun = db.Racuns.Find(id);
            db.Racuns.Remove(racun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }*/

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
