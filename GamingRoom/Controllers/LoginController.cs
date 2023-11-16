using GamingRoom.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace GamingRoom.Controllers
{
    public class LoginController : Controller
    {
        private ModelDBContext db = new ModelDBContext();

        [HttpGet]
        // Questo metodo gestisce la richiesta GET per la pagina di accesso.
        public ActionResult Login(string returnUrl = "")
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // Questo metodo gestisce la richiesta POST per l'accesso dell'utente.
        public ActionResult Login(UserCustomer user, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                UserCustomer u = db.UserCustomers.FirstOrDefault(ut => ut.Email == user.Email && ut.Password == user.Password);
                if (u != null)
                {
                    // Autenticazione dell'utente tramite Forms Authentication.
                    FormsAuthentication.SetAuthCookie(u.Email, false);

                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Credenziali non valide.");
                }
            }
            return View(user);
        }

        // Questo metodo gestisce la richiesta per il logout dell'utente.
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }
    }
}
