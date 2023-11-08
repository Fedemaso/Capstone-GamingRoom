using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GamingRoom.Models;
using System.Web.Mvc;

namespace GamingRoom.Controllers
{
    public class RegisterController : Controller
    {
         ModelDBContext db = new ModelDBContext();

        public ActionResult Index()
        {
            return View();
        }



        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register([Bind(Exclude = "Role")] UserCustomer user)
        {

            user.Role = "User";
            if (ModelState.IsValid)
            {
                db.UserCustomers.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
