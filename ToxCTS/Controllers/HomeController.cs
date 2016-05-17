using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ToxCTS.Controllers
{
    public class HomeController : Controller
    {
        private List<ToxCTS.Models.Chemical> Chemicals;

        public ActionResult Index()
        {
            Chemicals = new List<ToxCTS.Models.Chemical>();
            for (int i = 1; i < 101; i++)
            {
                ToxCTS.Models.Chemical chem = new Models.Chemical();
                chem.CommonName.Add("Placeholder");
                chem.ChemName = "Placeholder";
                chem.CAS = i;
                Chemicals.Add(chem);
            }
            ViewBag.Message = "";
            if (Chemicals.Count == 0)
            {
                ViewBag.Message = "We're sorry, there seems to be an issue with our Databases.  Please check back later and email IT.";
            }
            return View(Chemicals);
        }

        public ActionResult About()
        {     
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Details(int? id)
        {
            ToxCTS.Models.Chemical chem = new Models.Chemical();
            chem.CommonName.Add("Placeholder");
            chem.ChemName = "Placeholder";
            return View(chem);
        }

    }
}
