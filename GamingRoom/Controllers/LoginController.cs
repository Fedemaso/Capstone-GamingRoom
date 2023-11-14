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
        public ActionResult Login(string returnUrl = "")
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserCustomer user, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                UserCustomer u = db.UserCustomers.FirstOrDefault(ut => ut.Email == user.Email && ut.Password == user.Password);
                if (u != null)
                {
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


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }
    }
}