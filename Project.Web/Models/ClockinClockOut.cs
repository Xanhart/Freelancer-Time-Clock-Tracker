using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Web.Models
{
    public class ClockinClockOut
    {
        public string UserName { get; set; }
        public bool CanClockIn { get; set; }
        public string Notes { get; set; }
    }
}