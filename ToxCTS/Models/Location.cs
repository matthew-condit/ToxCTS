using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ToxCTS.Models
{
    public class Location
    {
        [Display(Name = "Room #")]
        public string room { get; set; }

        [Display(Name = "Cabinet")]
        public string cabinet { get; set; }


        public Location()
        {
            this.room = "0";
            this.cabinet = "";
        }
    }
}