using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GamingRoom.Models;

namespace GamingRoom.Controllers
{


    public class EventsController : Controller
    {
        private ModelDBContext db = new ModelDBContext();

        // GET: Events
        public ActionResult Index()
        {
            var events = db.Events.Include(e => e.Users).Include(e => e.Venues).Include(e => e.Teams);
            return View(events.ToList());
        }


        [Authorize(Roles = "SuperAdmin")]

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Events events = db.Events.Include(e => e.Teams).SingleOrDefault(e => e.EventID == id);
            if (events == null)
            {
                return HttpNotFound();
            }
            return View(events);
        }



        [Authorize(Roles = "SuperAdmin")]

        // GET: Events/Create
        public ActionResult Create()
        {
            ViewBag.VenueID = new SelectList(db.Venues, "VenueID", "Name");
            ViewBag.CreatedBy = new SelectList(db.Users, "UserID", "Username");
            ViewBag.Teams = db.Teams.ToList();
            return View();
        }



        [Authorize(Roles = "SuperAdmin")]

        // POST: Events/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Description,Date,VenueID,TicketsAvailable,TicketsSold,IsActive,CreatedBy,Photo,TicketPrice")] Events events, HttpPostedFileBase eventPhoto, int[] selectedTeams)
        {
            if (ModelState.IsValid)
            {
                if (eventPhoto != null && eventPhoto.ContentLength > 0)
                {
                    events.Photo = eventPhoto.FileName;
                    string pathToSave = Server.MapPath("~/Content/ImgEvents/") + eventPhoto.FileName;
                    eventPhoto.SaveAs(pathToSave);
                }

                if (selectedTeams != null)
                {
                    events.Teams = new List<Teams>();
                    foreach (var teamId in selectedTeams)
                    {
                        var teamToAdd = db.Teams.Find(teamId);
                        events.Teams.Add(teamToAdd);
                    }
                }

                db.Events.Add(events);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.VenueID = new SelectList(db.Venues, "VenueID", "Name", events.VenueID);
            ViewBag.CreatedBy = new SelectList(db.Users, "UserID", "Username", events.CreatedBy);
            ViewBag.Teams = db.Teams.ToList();
            return View(events);
        }



        [Authorize(Roles = "SuperAdmin")]

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Events events = db.Events.Include(e => e.Teams).Where(e => e.EventID == id).Single();
            if (events == null)
            {
                return HttpNotFound();
            }
            ViewBag.CreatedBy = new SelectList(db.Users, "UserID", "Username", events.CreatedBy);
            ViewBag.VenueID = new SelectList(db.Venues, "VenueID", "Name", events.VenueID);
            ViewBag.Teams = db.Teams.ToList();
            return View(events);
        }




        [Authorize(Roles = "SuperAdmin")]

        // POST: Events/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventID,Name,Description,Date,VenueID,TicketsAvailable,TicketsSold,IsActive,CreatedBy,Photo,TicketPrice")] Events events, HttpPostedFileBase eventPhoto, int[] selectedTeams)
        {
            if (ModelState.IsValid)
            {
                // Trova l'evento esistente nel database
                var eventToUpdate = db.Events.Include(i => i.Teams).Where(i => i.EventID == events.EventID).Single();

                // Se è stato caricato un nuovo file di foto, salvalo e aggiorna il percorso nel database
                if (eventPhoto != null && eventPhoto.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(eventPhoto.FileName);
                    var pathToSave = Path.Combine(Server.MapPath("~/Content/ImgEvents/"), fileName);
                    eventPhoto.SaveAs(pathToSave);
                    eventToUpdate.Photo = fileName; // Aggiorna l'entità tracciata
                }

                // Aggiorna le altre proprietà dell'evento
                eventToUpdate.Name = events.Name;
                eventToUpdate.Description = events.Description;
                eventToUpdate.Date = events.Date;
                eventToUpdate.VenueID = events.VenueID;
                eventToUpdate.TicketsAvailable = events.TicketsAvailable;
                eventToUpdate.TicketsSold = events.TicketsSold;
                eventToUpdate.IsActive = events.IsActive;
                eventToUpdate.CreatedBy = events.CreatedBy;
                eventToUpdate.TicketPrice = events.TicketPrice; // Aggiorna il prezzo del biglietto

                // Aggiorna la lista delle squadre associate all'evento
                UpdateEventTeams(selectedTeams, eventToUpdate);

                // Salva le modifiche nel database
                db.Entry(eventToUpdate).State = EntityState.Modified;
                db.SaveChanges();

                // Reindirizza all'indice
                return RedirectToAction("Index");
            }

            // Se il modello non è valido, ricarica la vista con le informazioni necessarie
            ViewBag.CreatedBy = new SelectList(db.Users, "UserID", "Username", events.CreatedBy);
            ViewBag.VenueID = new SelectList(db.Venues, "VenueID", "Name", events.VenueID);
            ViewBag.Teams = db.Teams.ToList();
            return View(events);
        }




        [Authorize(Roles = "SuperAdmin")]

        private void UpdateEventTeams(int[] selectedTeams, Events eventToUpdate)
        {
            if (selectedTeams == null)
            {
                eventToUpdate.Teams = new List<Teams>();
                return;
            }

            var selectedTeamsHS = new HashSet<int>(selectedTeams);
            var eventTeams = new HashSet<int>(eventToUpdate.Teams.Select(c => c.TeamID));

            foreach (var team in db.Teams)
            {
                if (selectedTeamsHS.Contains(team.TeamID))
                {
                    if (!eventTeams.Contains(team.TeamID))
                    {
                        eventToUpdate.Teams.Add(team);
                    }
                }
                else
                {
                    if (eventTeams.Contains(team.TeamID))
                    {
                        eventToUpdate.Teams.Remove(team);
                    }
                }
            }
        }




        [Authorize(Roles = "SuperAdmin")]
                // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Events events = db.Events.Find(id);
            if (events == null)
            {
                return HttpNotFound();
            }
            return View(events);
        }



        [Authorize(Roles = "SuperAdmin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Events events = db.Events.Find(id);
            if (events != null)
            {
                db.Events.Remove(events);
                db.SaveChanges();
            }
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



        public ActionResult UserDetails(int id)
        {
            var evento = db.Events.Include(e => e.Venues).Include(e => e.Teams).FirstOrDefault(e => e.EventID == id);
            if (evento == null)
            {
                return HttpNotFound();
            }
            return View(evento);
        }


        public ActionResult AllEvents()
        {
            var events = db.Events
                           .Include(e => e.Venues)
                           .Include(e => e.Teams)
                           .ToList();

            return View(events);
        }

    }
}
