using Project.Web.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Web.Controllers;
using Project.Web.Models;

namespace Project.Web.Controllers
{
    public class FreeLancerController : Controller
    {
        private IFreeLancerDAL dal;
        public FreeLancerController(IFreeLancerDAL dal)
        {
            this.dal = dal;
        }



        // GET: FreeLancer
        public ActionResult Login()
        {
            TimeCard time = new TimeCard();
            return View("Login", time);
        }
        [HttpGet]
        public ActionResult Main(TimeCard model)
        {

            // if we have a username on ht emodel put it in session
            // get a username out of session then

            if (!String.IsNullOrEmpty(model.Username))
            {
                Session["username"] = model.Username;
            }

            string username = Session["username"] as string;



            List<TimeCard> cards = dal.GetLastTimeCard(username);

            return View("Main", cards);
            //return View("Main", );
        }
        public ActionResult Clock(string username)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login");
            }

            ClockinClockOut clk = new ClockinClockOut();
            username = Session["username"] as string;
            clk.CanClockIn = dal.CanClockIn(username);
            clk.UserName = username;
            return View("Clock", clk);
        }

        [HttpPost]
        public ActionResult ClockIn(ClockinClockOut model)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login");
            }

            TimeCard tc = new TimeCard();
            tc.Username = Session["username"] as string;
            tc.Notes = model.Notes;

            dal.ClockIn(tc); // 

            return RedirectToAction("Main");
        }
        [HttpPost]
        public ActionResult ClockOut(ClockinClockOut Model)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login");
            }
            Model.UserName = Session["username"] as string;

            dal.ClockOut(Model.UserName); // 

            return RedirectToAction("Main");
        }


    }
}