using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ToxCTS.Controllers
{
    public class AdminController : Controller
    {
        private Models.Chemical EditChem;
        private Models.Chemical UploadStageChem;
        private Models.Chemical CreatedChem;


        //
        // GET: /Admin/

        public ActionResult Index()
        {

            return View();
        }

        //
        // GET: /Admin/Create
        public ActionResult Create()
        {
            return View();
        }
        //
        // GET: /Admin/Created
        public ActionResult Created()
        {
            return View();
        }
        //
        // GET: /Admin/Upload
        public ActionResult Upload(string amount, string chemName, string commName,
            string ContSize, string ContUnit, string ContType, string CSCnum, string CASnum, 
            string Manufacturer, string ExpDate, string RoomNum, string Cabinet)
        {
            UploadStageChem = new Models.Chemical();
            UploadStageChem.Amount = Double.Parse(amount);
            UploadStageChem.ChemName = chemName;
            UploadStageChem.CommonName.Add(commName);
            UploadStageChem.ChemContainer.Size = Int16.Parse(ContSize);
            UploadStageChem.ChemContainer.Unit = ContUnit;
            UploadStageChem.ChemContainer.Type = ContType;
            UploadStageChem.CSC = int.Parse(CSCnum);
            UploadStageChem.CAS = int.Parse(CASnum);
            UploadStageChem.Manufacturer = Manufacturer;
            UploadStageChem.ExpDate = DateTime.Parse(ExpDate);
            UploadStageChem.location.room = RoomNum;
            UploadStageChem.location.cabinet = Cabinet;
            UploadStageChem.ID = HomeController.nextID;
            HomeController.nextID++;

            HomeController.Chemicals.Add(UploadStageChem);
            return View(UploadStageChem);
        }
        //
        // GET: /Admin/Updated
        public ActionResult Updated()
        {
            CreatedChem = new Models.Chemical();
            return View(CreatedChem);
        }
        //
        // POST: /Admin/Created
        [HttpPost]
        public ActionResult Created(String ID, String FileUpload)
        {

            CreatedChem = HomeController.getChemById(int.Parse(ID));
            return View(CreatedChem);
        }

        //
        // GET: /Admin/Edit
        public ActionResult Edit(String id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                EditChem = HomeController.getChemById(int.Parse(id));
                return View(EditChem);
            }
            return View(new Models.Chemical());
        }

        // 
        // GET: /Admin/Search 
        public ActionResult Search()
        {
            return View();
        }

        // GET:  /Admin/Results
        public ActionResult Results(string commonName, string chemicalName, string CASnum, string CSCnum)
        {
            List<Models.Chemical> chems = HomeController.Chemicals;
            List<Models.Chemical> SearchResults = new List<Models.Chemical>();
            foreach (Models.Chemical chem in chems)
            {
                if (!String.IsNullOrEmpty(commonName))
                {
                    if (!chem.CommonName.Equals(commonName))
                    {
                        continue;
                    }
                }
                if (!String.IsNullOrEmpty(chemicalName))
                {
                    if (!chem.ChemName.Equals(chemicalName))
                    {
                        continue;
                    }
                }
                if (!String.IsNullOrEmpty(CASnum))
                {
                    if (chem.CAS != int.Parse(CASnum))
                    {
                        continue;
                    }
                }
                if (!String.IsNullOrEmpty(CSCnum))
                {
                    if (chem.CSC != int.Parse(CSCnum))
                    {
                        continue;
                    }

                }
                SearchResults.Add(chem);
            }
            if (SearchResults.Count ==0) { ViewBag.Message = "No Results for this search."; }
            return View(SearchResults);
        }
    }
}
