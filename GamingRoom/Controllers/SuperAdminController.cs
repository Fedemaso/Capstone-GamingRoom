using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GamingRoom.Controllers
{

    [Authorize(Roles = "SuperAdmin")]
    public class SuperAdminController : Controller
    {
        // GET: Admin
        public ActionResult SuperAdminView()
        {
            return View();
        }

        public ActionResult SuperAdminMenu()
        {
            return View();
        }



    }
}