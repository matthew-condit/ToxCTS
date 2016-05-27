using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.IO;

namespace ToxCTS.Controllers
{
    public class HomeController : Controller
    {
        private static List<ToxCTS.Models.Chemical> Chemicals = new List<ToxCTS.Models.Chemical>();
        private static int nextID = 1;

        
        //
        //  Gets Chemicals 
        public static List<ToxCTS.Models.Chemical> getChemicals()
        {
            return Chemicals;
        }

        //
        // Adds new Chemical to Chemicals 
        public static bool addChemical(Models.Chemical chem)
        {
            try
            {
                Chemicals.Add(chem);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        //
        // Find Chem based on Id
        public static ToxCTS.Models.Chemical getChemById(int ID)
        {
            Debug.WriteLine(ID);
            foreach (Models.Chemical chem in Chemicals) {
                Debug.WriteLine(chem.ID);
                if (chem.ID == ID)
                {
                    return chem;
                }
            }
            return new Models.Chemical();
        }

        public static bool deleteChemById(int ID)
        {
            try
            {

                int removed = Chemicals.RemoveAll(item => item.ID == ID);
                if (removed == 1) return true;
                return false;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return false;
            }

        }

        //
        //  Assign the next ID for a newly created Chemical 
        public static int getNextID()
        {
            nextID++;
            return nextID - 1;
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
                    if (!chem.CommonNames.Equals(ChemNameSearch))
                    {
                        continue;
                    }
                }
                if (!String.IsNullOrEmpty(CASSearch))
                {
                    if (!chem.CAS.Equals(CASSearch))
                    {
                        continue;
                    }
                }
                if (!String.IsNullOrEmpty(CSCSearch))
                {
                    if (!chem.CSC.Equals(CSCSearch))
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

        //
        //Open That sucker Up
        public ActionResult OpenFile(string fileName, string filePath)
        {
            filePath = filePath.Replace(@"N:\SPONSORS\", @"\\toxfs\Protocol\SPONSORS\");
            filePath = filePath.Replace(@"Z:\", @"\\toxfs\Pharma File\");

            FileInfo fileInfo = new FileInfo(filePath);
            if (!fileInfo.Exists)
            {
                return RedirectToAction("Index", "Error", new { status = 400 });
            }

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return new FileStreamResult(fileStream, "application/pdf");
        }

    }
}
