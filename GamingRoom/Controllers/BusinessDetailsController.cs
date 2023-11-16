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

  
        public class BusinessDetailsController : Controller
        {
            private ModelDBContext db = new ModelDBContext();

            // GET: BusinessDetails
            // Questo metodo gestisce la richiesta GET per la pagina principale del controller.
            public ActionResult Index()
            {
                // Recupera tutti i dettagli dell'azienda dal database e li restituisce alla vista.
                var businessDetails = db.BusinessDetails.Include(b => b.Users);
                return View(businessDetails.ToList());
            }

            // GET: BusinessDetails/Details/5
            // Questo metodo gestisce la richiesta GET per visualizzare i dettagli di un'azienda specifica.
            public ActionResult Details(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                // Cerca i dettagli dell'azienda con l'ID specificato nel database.
                BusinessDetails businessDetails = db.BusinessDetails.Find(id);

                if (businessDetails == null)
                {
                    return HttpNotFound();
                }

                return View(businessDetails);
            }

            // GET: BusinessDetails/Create
            // Questo metodo gestisce la richiesta GET per la creazione di nuovi dettagli aziendali.
            public ActionResult Create()
            {
                // Prepara la vista per la creazione di nuovi dettagli aziendali, fornendo una lista di utenti.
                ViewBag.BusinessID = new SelectList(db.Users, "UserID", "Username");
                return View();
            }

            // POST: BusinessDetails/Create
            // Questo metodo gestisce la richiesta POST per la creazione di nuovi dettagli aziendali.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Create([Bind(Include = "BusinessID,BusinessName,Address,PhoneNumber,Website")] BusinessDetails businessDetails)
            {
                if (ModelState.IsValid)
                {
                    // Aggiunge i nuovi dettagli aziendali al database e reindirizza alla pagina principale.
                    db.BusinessDetails.Add(businessDetails);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                // Se la creazione non ha avuto successo, prepara la vista con i dati inseriti e un elenco di utenti.
                ViewBag.BusinessID = new SelectList(db.Users, "UserID", "Username", businessDetails.BusinessID);
                return View(businessDetails);
            }

            // GET: BusinessDetails/Edit/5
            // Questo metodo gestisce la richiesta GET per la modifica dei dettagli di un'azienda specifica.
            public ActionResult Edit(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                // Cerca i dettagli dell'azienda con l'ID specificato nel database per la modifica.
                BusinessDetails businessDetails = db.BusinessDetails.Find(id);

                if (businessDetails == null)
                {
                    return HttpNotFound();
                }

                // Prepara la vista per la modifica dei dettagli aziendali, fornendo una lista di utenti.
                ViewBag.BusinessID = new SelectList(db.Users, "UserID", "Username", businessDetails.BusinessID);
                return View(businessDetails);
            }

            // POST: BusinessDetails/Edit/5
            // Questo metodo gestisce la richiesta POST per la modifica dei dettagli aziendali.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Edit([Bind(Include = "BusinessID,BusinessName,Address,PhoneNumber,Website")] BusinessDetails businessDetails)
            {
                if (ModelState.IsValid)
                {
                    // Aggiorna i dettagli aziendali nel database e reindirizza alla pagina principale.
                    db.Entry(businessDetails).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                // Se la modifica non ha avuto successo, prepara la vista con i dati modificati e un elenco di utenti.
                ViewBag.BusinessID = new SelectList(db.Users, "UserID", "Username", businessDetails.BusinessID);
                return View(businessDetails);
            }

            // GET: BusinessDetails/Delete/5
            // Questo metodo gestisce la richiesta GET per la cancellazione dei dettagli di un'azienda specifica.
            public ActionResult Delete(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                // Cerca i dettagli dell'azienda con l'ID specificato nel database per la cancellazione.
                BusinessDetails businessDetails = db.BusinessDetails.Find(id);

                if (businessDetails == null)
                {
                    return HttpNotFound();
                }

                return View(businessDetails);
            }

            // POST: BusinessDetails/Delete/5
            // Questo metodo gestisce la richiesta POST per la conferma della cancellazione dei dettagli aziendali.
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public ActionResult DeleteConfirmed(int id)
            {
                // Rimuove i dettagli aziendali con l'ID specificato dal database e reindirizza alla pagina principale.
                BusinessDetails businessDetails = db.BusinessDetails.Find(id);
                db.BusinessDetails.Remove(businessDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

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


