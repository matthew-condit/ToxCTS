using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ToxCTS.Controllers
{
    public class AdminController : Controller
    {
        private List<ToxCTS.Models.Chemical> SearchResults;
        private Models.Chemical EditChem;

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
        // GET: /Admin/Edit
        public ActionResult Edit()
        {
            EditChem = new Models.Chemical();
            return View(EditChem);
        }

        // 
        // GET: /Admin/Search 
        public ActionResult Search()
        {
            return View();
        }

        // GET:  /Admin/Results
        public ActionResult Results()
        {
            SearchResults = new List<ToxCTS.Models.Chemical>();
            for (int i = 1; i < 101; i++)
            {
                ToxCTS.Models.Chemical chem = new Models.Chemical();
                chem.CAS = i;
                SearchResults.Add(chem);
            }
            if (SearchResults.Count == 0) { ViewBag.Message = "No Results for this search."; }
            return View(SearchResults);
        }
    }
}
