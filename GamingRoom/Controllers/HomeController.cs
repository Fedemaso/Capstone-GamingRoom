using GamingRoom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;





namespace GamingRoom.Controllers
{



    public class HomeController : Controller
    {

        private ModelDBContext db = new ModelDBContext();

        
        public ActionResult Index()
        {
            var events = db.Events
                           .Include(e => e.Venues)
                           .Include(e => e.Teams)
                           .ToList();
            var teams = events.SelectMany(e => e.Teams).Distinct().ToList();

            ViewBag.Teams = teams; // Passa i team alla view tramite ViewBag
            return View(events);
        }


       

    }
}