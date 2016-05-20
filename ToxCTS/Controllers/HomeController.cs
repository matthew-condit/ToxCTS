using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ToxCTS.Controllers
{
    public class HomeController : Controller
    {
        public static List<ToxCTS.Models.Chemical> Chemicals = new List<ToxCTS.Models.Chemical>();
        public static int nextID = 1;

        

        //
        // Find Chem based on Id
        public static ToxCTS.Models.Chemical getChemById(int ID)
        {
            foreach (Models.Chemical chem in Chemicals) {
                if (chem.ID == ID)
                {
                    return chem;
                }
            }
            return new Models.Chemical();
        }
        //
        // GET: /Home/
        public ActionResult Index()
        {

            if (Chemicals.Count == 0)
            {
                ViewBag.Message = "We're sorry, there seems to be an issue with our Database.  Please check back later and email IT.";
            }
            return View(Chemicals);
        }
        //
        // GET: /Home/Results
        public ActionResult Results(string ChemNameSearch, string CSCSearch, string CASSearch)
        {
            List<Models.Chemical> results = new List<Models.Chemical>();
            List<Models.Chemical> chems = HomeController.Chemicals;
            foreach (Models.Chemical chem in chems)
            {
                if (!String.IsNullOrEmpty(ChemNameSearch))
                {
                    if (!chem.CommonName.Equals(ChemNameSearch))
                    {
                        continue;
                    }
                }
                if (!String.IsNullOrEmpty(CASSearch))
                {
                    if (chem.CAS != int.Parse(CASSearch))
                    {
                        continue;
                    }
                }
                if (!String.IsNullOrEmpty(CSCSearch))
                {
                    if (chem.CSC != int.Parse(CSCSearch))
                    {
                        continue;
                    }

                }
                results.Add(chem);
            }
            if (results.Count == 0)
            {
                ViewBag.Message = "No Chemicals matched your search criterial.  If this seems incorrect, please contact IT.";
            }
            return View(results);
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
            foreach (Models.Chemical chem in Chemicals)
            {
                if (chem.ID == id)
                {
                    return View(chem);
                }
            }
            return View(new Models.Chemical());
        }

    }
}
