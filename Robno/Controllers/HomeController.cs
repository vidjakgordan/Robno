using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Robno.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "O aplikaciji Robno";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Kontakt stranica";

            return View();
        }

        //dodano za racun
        public ActionResult Racun()
        {
            string apiUri = Url.HttpRouteUrl("DefaultApi", new { controller = "Racun", });
            ViewBag.ApiUrl = new Uri(Request.Url, apiUri).AbsoluteUri.ToString();

            return View();
        }

        //dodano za primku
        public ActionResult Primka()
        {
            string apiUri = Url.HttpRouteUrl("DefaultApi", new { controller = "Primka", });
            ViewBag.ApiUrl = new Uri(Request.Url, apiUri).AbsoluteUri.ToString();

            return View();
        }

    }
}