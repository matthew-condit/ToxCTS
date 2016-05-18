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


        //
        // GET: /
        public ActionResult Index()
        {
            Chemicals = new List<ToxCTS.Models.Chemical>();
            for (int i = 1; i < 1001; i++)
            {
                ToxCTS.Models.Chemical chem = new Models.Chemical();
                chem.CommonName.Add("Placeholder");
                chem.ChemName = "Placeholder";
                chem.CAS = i;
                Chemicals.Add(chem);
            }
            if (Chemicals.Count == 0)
            {
                ViewBag.Message = "We're sorry, there seems to be an issue with our Databases.  Please check back later and email IT.";
            }
            return View(Chemicals);
        }

        //
        // GET: /Home/Contact
        public ActionResult Contact()
        {
            return View();
        }
        
        //
        // GET: /Home/Details
        public ActionResult Details(int? id)
        {
            ToxCTS.Models.Chemical chem = new Models.Chemical();
            chem.CommonName.Add("Placeholder");
            chem.ChemName = "Ethyanol";
            return View(chem);
        }

    }
}
