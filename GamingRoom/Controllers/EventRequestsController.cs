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
        public ActionResult Create()
        {
            return View();
        }

        // POST: EventRequests/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Description,ProposedDate,VenueProposal,ProposedTicketsAvailable,ProposedTicketPrice")] EventRequest eventRequest)
        {
            if (ModelState.IsValid)
            {
                db.EventRequests.Add(eventRequest);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(eventRequest);
        }

        // GET: EventRequests for SuperAdmin
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult Index()
        {
            return View(db.EventRequests.ToList());
        }

        // GET: EventRequests/Approve/5
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


            // Converte la richiesta in un vero evento
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