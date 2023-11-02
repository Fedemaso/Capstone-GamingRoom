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
        private ModelDBContext db = new ModelDBContext();

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserCustomer user)
        {
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
