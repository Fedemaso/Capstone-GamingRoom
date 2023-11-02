using GamingRoom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;


//namespace GamingRoom.Controllers
//{
//    public class HomeController : Controller
//    {
//        [Authorize(Roles = "User")]
//        public ActionResult UserPanel()
//        {
//            return View();
//        }

//        [Authorize(Roles = "Admin,SuperAdmin")]
//        public ActionResult AdminPanel()
//        {
//            return View();
//        }

//        [Authorize(Roles = "SuperAdmin")]
//        public ActionResult SuperAdminPanel()
//        {
//            return View();
//        }
//    }

//}


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
            return View(events);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}