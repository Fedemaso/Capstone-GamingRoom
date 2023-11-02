using GamingRoom.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace GamingRoom.Controllers
{
    public class LoginController : Controller
    {
        private ModelDBContext db = new ModelDBContext();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserCustomer user)
        {
            if (ModelState.IsValid)
            {
                var u = db.UserCustomers.Where(ut => ut.Email == user.Email && ut.Password == user.Password).FirstOrDefault();
                if (u != null)
                {
                    FormsAuthentication.SetAuthCookie(u.Email, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Credenziali non valide. Controlla che le tue credenziali siano state inserite correttamente.");
                    return View();
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }
    }
}


