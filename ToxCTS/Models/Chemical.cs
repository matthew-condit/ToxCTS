using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace ToxCTS.Models
{
    public class Chemical
    {
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Display(Name = "Amount")]
        public double Amount { get; set; }

        [Display(Name = "Chemical Name")]
        public String ChemName { get; set; }

        [Display(Name = "Common Name")]
        public List<String> CommonName { get; set; }

        [Display(Name= "Container")]
        public Container ChemContainer {get; set;}

        [Display(Name = "CSC Number")]
        public String CSC { get; set; }

        [Display(Name = "CAS Number")]
        public String CAS { get; set; }

        [Display(Name = "Manufacturer")]
        public String Manufacturer { get; set; }

        [Display(Name = "Exp. Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ExpDate { get; set; }

        [Display(Name = "GHS Hazard Classifications")]
        public List<String> GHS { get; set; }

        [Display(Name = "Hazards")]
        public List<String> Hazards { get; set; }

        [Display(Name = "Location")]
        public Location location { get; set; }

        [Display(Name = "File Name")]
        public String FileName { get; set; }

        [Display(Name = "File Path")]
        public String FilePath { get; set; }

        //
        // Creates bland new Chemical
        public Chemical()
        {
            this.ID = -1;
            this.Amount = 0.0;
            this.ChemName = "";
            this.CommonName = new List<String>();
            this.ChemContainer = new Container();
            this.CSC = "-1";
            this.CAS = "-1";
            this.Manufacturer = "";
            this.ExpDate = DateTime.Now;
            this.GHS = new List<string>();
            this.Hazards = new List<string>();
            this.location = new Location();
            this.FileName = "";
            this.FilePath = "C:/";
        }
    }

}