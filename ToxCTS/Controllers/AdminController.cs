using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.IO;

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
        // GET: /Admin/Upload
        public ActionResult Upload(string chemName, string commName,
            string ContSize, string ContUnit, string ContType, string CSCnum, string CASnum, 
            string Manufacturer, string ExpDate, string RoomNum, string Cabinet, 
            bool? health, bool? flame, bool? corrosion, bool? exclamation, string amount =  "0")
        {
            UploadStageChem = new Models.Chemical();
            try { UploadStageChem.Amount = Double.Parse(amount); }
            catch { }
            UploadStageChem.ChemName = chemName;
            UploadStageChem.CommonNames = commName.Split(',').Select(p => p.Trim()).ToList();
            try { UploadStageChem.ChemContainer.Size = Int16.Parse(ContSize); }
            catch { }
            UploadStageChem.ChemContainer.Unit = ContUnit;
            UploadStageChem.ChemContainer.Type = ContType;
            UploadStageChem.CSC = CSCnum;
            UploadStageChem.CAS = CASnum;
            UploadStageChem.Manufacturer = Manufacturer;
            UploadStageChem.ExpDate = DateTime.Parse(ExpDate);
            UploadStageChem.location.room = RoomNum;
            UploadStageChem.location.cabinet = Cabinet;
            UploadStageChem.ID = HomeController.getNextID();
            if (isTrue(health)) { UploadStageChem.Hazards.Add("health.png"); }
            if (isTrue(flame)) { UploadStageChem.Hazards.Add("flame.png"); }
            if (isTrue(corrosion)) { UploadStageChem.Hazards.Add("corrosion.png"); }
            if (isTrue(exclamation)) { UploadStageChem.Hazards.Add("exclamation.png"); }

            HomeController.addChemical(UploadStageChem);
            return View(UploadStageChem);
        }

        //
        // GET:  /Admin/Updated
        public ActionResult Updated()
        {
            return View(new Models.Chemical());
        }
        //
        // POST: /Admin/Updated
        [HttpPost]
        public ActionResult Updated(String ID, HttpPostedFileBase FileUpload)
        {
            CreatedChem = HomeController.getChemById(int.Parse(ID));

            if (!HttpIsNull(FileUpload))
            {
                if (FileUpload.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(FileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                    FileUpload.SaveAs(path);
                    CreatedChem.FileName = fileName;
                    CreatedChem.FilePath = path;
                }
            }
            return View(CreatedChem);
        }
        //
        // POST: /Admin/SDS
        public ActionResult SDS(string id, string amount, string chemName, string commName,
            string ContSize, string ContUnit, string ContType, string CSCnum, string CASnum,
            string Manufacturer, string ExpDate, string RoomNum, string Cabinet,
            bool? health, bool? flame, bool? corrosion, bool? exclamation)
        {
            ToxCTS.Models.Chemical updatedChem = HomeController.getChemById(int.Parse(id));

            try { updatedChem.Amount = Double.Parse(amount); }
            catch { }
            updatedChem.ChemName = chemName;
            updatedChem.CommonNames = commName.Split(',').Select(p => p.Trim()).ToList();
            try { updatedChem.ChemContainer.Size = Int16.Parse(ContSize); }
            catch { }
            updatedChem.ChemContainer.Unit = ContUnit;
            updatedChem.ChemContainer.Type = ContType;
            updatedChem.CSC = CSCnum;
            updatedChem.CAS = CASnum;
            updatedChem.Manufacturer = Manufacturer;
            updatedChem.ExpDate = DateTime.Parse(ExpDate);
            updatedChem.location.room = RoomNum;
            updatedChem.location.cabinet = Cabinet;
            updatedChem.Hazards = new List<string>();
            if (isTrue(health)) { updatedChem.Hazards.Add("health.png"); }
            if (isTrue(flame)) { updatedChem.Hazards.Add("flame.png"); }
            if (isTrue(corrosion)) { updatedChem.Hazards.Add("corrosion.png"); }
            if (isTrue(exclamation)) { updatedChem.Hazards.Add("exclamation.png"); }

            return View(updatedChem);
        }
        //
        // GET: /Admin/Created
        public ActionResult Created()
        {
            return View(new Models.Chemical());
        }

        //
        // POST: /Admin/Created
        [HttpPost]
        public ActionResult Created(String ID, HttpPostedFileBase FileUpload)
        {
            CreatedChem = HomeController.getChemById(int.Parse(ID));
            
            if (!HttpIsNull(FileUpload))
            {
                if (FileUpload.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(FileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                    FileUpload.SaveAs(path);
                    CreatedChem.FileName = fileName;
                    CreatedChem.FilePath = path;
                }
            }
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

        //
        // GET:  /Admin/Results
        public ActionResult Results(string commonName, string chemicalName, string CASnum, string CSCnum)
        {
            List<Models.Chemical> chems = HomeController.getChemicals();
            List<Models.Chemical> SearchResults = new List<Models.Chemical>();
            foreach (Models.Chemical chem in chems)
            {
                try
                {
                    if (!String.IsNullOrEmpty(commonName))
                    {
                        bool match = false;
                        foreach (string name in chem.CommonNames) {
                            Debug.WriteLine(name);
                            if (name.Length >= commonName.Length && String.Compare(name.Substring(0, commonName.Length), commonName, true) == 0)
                            {
                                match = true;
                            }
                        }
                        if (!match) continue;
                    }
                    if (!String.IsNullOrEmpty(chemicalName))
                    {
                        if (chem.ChemName.Length >= chemicalName.Length && String.Compare(chem.ChemName.Substring(0, chemicalName.Length), chemicalName) != 0)
                        {
                            continue;
                        }
                    }
                    if (!String.IsNullOrEmpty(CASnum) && !String.IsNullOrEmpty(chem.CAS))
                    {
                        if (!String.IsNullOrEmpty(chem.CAS) && !chem.CAS.Equals(CASnum))
                        {
                            continue;
                        }
                    }
                    if (!String.IsNullOrEmpty(CSCnum))
                    {
                        if (!String.IsNullOrEmpty(chem.CSC) && !chem.CSC.Equals(CSCnum))
                        {
                            continue;
                        }

                    }
                    SearchResults.Add(chem);
                }
                catch (Exception e) { Console.WriteLine(e); }
            }
            if (SearchResults.Count ==0) { ViewBag.Message = "No Results for this search."; }
            return View(SearchResults);
        }

        //
        // GET: /Admin/Deleted 
        public ActionResult Deleted()
        {
            return View();
        }
        //
        // POST: /Admin/Deleted 
        [HttpPost]
        public ActionResult Deleted(string id)
        {
            bool successful = HomeController.deleteChemById(int.Parse(id));
            if (successful)
            {
                ViewBag.Message = "You have successfully deleted this chemical.";
            }
            else
            {
                ViewBag.Message = "There has been an issue deleting your chemical.  Please contact IT to resolve this issue.";
            }
            return View();
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

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        //
        //Helper, returns true only if boolean is defined and true
        public static bool isTrue(bool? b) {
            try
            {
                if (b == true)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
             
            }
            return false;
        }

        public static bool HttpIsNull(HttpPostedFileBase h) {
            try
            {
                if (! h.Equals(null)) return false;
            }
            catch (Exception e)
            {
            }
            return true;
        }
    }
 
}
