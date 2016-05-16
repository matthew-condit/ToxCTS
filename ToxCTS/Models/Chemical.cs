using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ToxCTS.Models
{
    public class Chemical
    {
        [Display(Name = "Amount")]
        public double Amount { get; set; }

        [Display(Name = "Chemical Name")]
        public String ChemName { get; set; }

        [Display(Name = "Common Name")]
        public String CommonName { get; set; }

        [Display(Name= "Container")]
        public Container ChemContainer {get; set;}

        [Display(Name = "CSC Number")]
        public int CSC { get; set; }

        [Display(Name = "CAS Number")]
        public int CAS { get; set; }

        [Display(Name = "Manufacturer")]
        public String Manufacturer { get; set; }

        [Display(Name = "Exp. Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ExpDate { get; set; }


        public Chemical()
        {
            this.Amount = 0.0;
            this.ChemName = "";
            this.CommonName = "";
            this.ChemContainer = new Container();
            this.CSC = -1;
            this.CAS = -1;
            this.Manufacturer = "";
            this.ExpDate = DateTime.Now;
        }
    }
}