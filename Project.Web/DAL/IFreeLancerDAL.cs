using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Web.Models;

namespace Project.Web.DAL
{
    public interface IFreeLancerDAL
    {
        List<TimeCard> GetLastTimeCard(string userName);
      
        void ClockIn(TimeCard t);
        void ClockOut(string username);
        bool CanClockIn(string userName);
    }
}
