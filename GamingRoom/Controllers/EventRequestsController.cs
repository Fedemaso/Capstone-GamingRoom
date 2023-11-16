using GamingRoom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GamingRoom.Controllers
{
    public class EventRequestsController : Controller
    {
        private ModelDBContext db = new ModelDBContext();

        // GET: EventRequests/Create
        // Questo metodo gestisce la richiesta GET per la creazione di una nuova richiesta di evento.
        public ActionResult Create()
        {
            return View();
        }

        // POST: EventRequests/Create
        // Questo metodo gestisce la richiesta POST per la creazione di una nuova richiesta di evento.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Description,ProposedDate,VenueProposal,ProposedTicketsAvailable,ProposedTicketPrice")] EventRequest eventRequest)
        {
            if (ModelState.IsValid)
            {
                // Aggiunge la nuova richiesta di evento al database e reindirizza alla pagina principale.
                db.EventRequests.Add(eventRequest);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(eventRequest);
        }

        // GET: EventRequests for SuperAdmin
        // Questo metodo gestisce la richiesta GET per visualizzare le richieste di evento per i SuperAdmin.
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult Index()
        {
            return View(db.EventRequests.ToList());
        }

        // GET: EventRequests/Approve/5
        // Questo metodo gestisce la richiesta GET per approvare una richiesta di evento.
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult Approve(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EventRequest eventRequest = db.EventRequests.Find(id);
            if (eventRequest == null)
            {
                return HttpNotFound();
            }

            int? venueId = null;
            if (int.TryParse(eventRequest.VenueProposal, out int parsedVenueId))
            {
                venueId = parsedVenueId;
            }

            // Converte la richiesta di evento in un vero evento e aggiunge al database.
            Events newEvent = new Events
            {
                Name = eventRequest.Name,
                Description = eventRequest.Description,
                Date = eventRequest.ProposedDate,
                VenueID = venueId,
                TicketsAvailable = eventRequest.ProposedTicketsAvailable,
                TicketPrice = eventRequest.ProposedTicketPrice,
            };

            db.Events.Add(newEvent);
            db.EventRequests.Remove(eventRequest);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: EventRequests/Delete/5
        // Questo metodo gestisce la richiesta GET per eliminare una richiesta di evento.
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EventRequest eventRequest = db.EventRequests.Find(id);
            if (eventRequest == null)
            {
                return HttpNotFound();
            }

            return View(eventRequest);
        }

        // POST: EventRequests/Delete/5
        // Questo metodo gestisce la richiesta POST per confermare l'eliminazione di una richiesta di evento.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult DeleteConfirmed(int id)
        {
            EventRequest eventRequest = db.EventRequests.Find(id);
            db.EventRequests.Remove(eventRequest);
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
