using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Web.Models
{
    public class TimeCard
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Project { get; set; }
        public DateTime ClockIn { get; set; }
        public DateTime? ClockOut { get; set; }
        public string Notes { get; set; }
        public double? Rate { get; set; }



    }
}