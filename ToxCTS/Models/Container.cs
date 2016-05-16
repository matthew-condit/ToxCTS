﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ToxCTS.Models
{
    public class Container
    {
        [Display(Name = "Size")]
        public double Size { get; set; }

        [Display(Name = "Unit")]
        public String Unit { get; set; }

        [Display(Name = "Type of Glass/Plastic")]
        public String Type { get; set; }

        public Container()
        {
            this.Size = 0;
            this.Unit = "N/a";
            this.Type = "N/a";
        }
    }
}