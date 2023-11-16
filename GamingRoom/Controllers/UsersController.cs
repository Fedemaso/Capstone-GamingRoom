using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GamingRoom.Models;

namespace GamingRoom.Controllers
{

    
        [Authorize(Roles = "SuperAdmin")]
        public class UsersController : Controller
        {
            private ModelDBContext db = new ModelDBContext();

            // Mostra un elenco di tutti gli utenti registrati.
            public ActionResult Index()
            {
                var users = db.Users.Include(u => u.BusinessDetails);
                return View(users.ToList());
            }

            // Visualizza i dettagli di un utente specifico.
            public ActionResult Details(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Users users = db.Users.Find(id);
                if (users == null)
                {
                    return HttpNotFound();
                }
                return View(users);
            }

            // Presenta un form per creare un nuovo utente.
            public ActionResult Create()
            {
                ViewBag.Roles = new SelectList(new List<string> { "User", "Admin", "SuperAdmin" });
                ViewBag.UserID = new SelectList(db.BusinessDetails, "BusinessID", "BusinessName");
                return View();
            }

            // Gestisce la richiesta POST per creare un nuovo utente.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Create([Bind(Include = "UserID,Username,Password,Email,Role")] Users users)
            {
                if (ModelState.IsValid)
                {
                    db.Users.Add(users);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.Roles = new SelectList(new List<string> { "User", "Admin", "SuperAdmin" });
                ViewBag.UserID = new SelectList(db.BusinessDetails, "BusinessID", "BusinessName", users.UserID);
                return View(users);
            }

            // Presenta un form per modificare un utente esistente.
            public ActionResult Edit(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Users user = db.Users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Roles = new SelectList(new List<string> { "User", "Admin", "SuperAdmin" }, user.Role);
                ViewBag.UserID = new SelectList(db.BusinessDetails, "BusinessID", "BusinessName", user.UserID);
                return View(user);
            }

            // Gestisce la richiesta POST per aggiornare un utente esistente.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Edit([Bind(Include = "UserID,Username,Password,Email,Role")] Users user)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.Roles = new SelectList(new List<string> { "User", "Admin", "SuperAdmin" }, user.Role);
                ViewBag.UserID = new SelectList(db.BusinessDetails, "BusinessID", "BusinessName", user.UserID);
                return View(user);
            }

            // Presenta un form di conferma per eliminare un utente.
            public ActionResult Delete(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Users users = db.Users.Find(id);
                if (users == null)
                {
                    return HttpNotFound();
                }
                return View(users);
            }

            // Gestisce la richiesta POST per eliminare definitivamente un utente.
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public ActionResult DeleteConfirmed(int id)
            {
                var user = db.Users.Include(u => u.BusinessDetails).FirstOrDefault(u => u.UserID == id);

                // Verifica se l'utente è associato a un'azienda
                if (user != null && user.BusinessDetails != null)
                {
                    // Imposta il messaggio di errore
                    TempData["ErrorMessage"] = "Non è possibile eliminare l'utente perché è associato a un'azienda.";
                    return RedirectToAction("Delete", new { id = id });
                }

                db.Users.Remove(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Metodo di pulizia delle risorse.
            protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
