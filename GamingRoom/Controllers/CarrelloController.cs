using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GamingRoom.Controllers
{
    public class CarrelloController : Controller
    {
        // GET: Carrello
        public ActionResult Index()
        {
            return View();
        }
    }
}