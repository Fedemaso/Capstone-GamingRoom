using GamingRoom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GamingRoom.Controllers
{
    public class ValidazioniController : Controller
    {
        //gestisce la validazioni lato client per i text input
        private static ModelDBContext db = new ModelDBContext();

        public ActionResult IsEmailValid(string email)
        {
            bool isValid = db.UserCustomers.All(x => x.Email != email); ;

            return Json(isValid, JsonRequestBehavior.AllowGet);
        }

       
    }
}