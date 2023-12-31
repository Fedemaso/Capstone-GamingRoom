﻿using System;
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

    [Authorize(Roles = "SuperAdmin")]

    public class VenuesController : Controller
    {
        private ModelDBContext db = new ModelDBContext();

        // GET: Venues
        public ActionResult Index()
        {
            return View(db.Venues.ToList());
        }

        // GET: Venues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venues venues = db.Venues.Find(id);
            if (venues == null)
            {
                return HttpNotFound();
            }
            return View(venues);
        }

        // GET: Venues/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VenueID,Name,Address,Capacity")] Venues venues, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null && image.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(image.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/ImgVenues"), fileName);
                    image.SaveAs(path);
                    venues.Photo = fileName;
                }

                db.Venues.Add(venues);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(venues);
        }

        // GET: Venues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venues venues = db.Venues.Find(id);
            if (venues == null)
            {
                return HttpNotFound();
            }
            return View(venues);
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VenueID,Name,Address,Capacity,ImageFileName")] Venues venues, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                var venueToUpdate = db.Venues.Find(venues.VenueID);

                if (venueToUpdate == null)
                {
                    return HttpNotFound();
                }

                // Aggiorna le proprietà di venueToUpdate
                venueToUpdate.Name = venues.Name;
                venueToUpdate.Address = venues.Address;
                venueToUpdate.Capacity = venues.Capacity;

                // Aggiorna l'immagine solo se ne viene fornita una nuova
                if (image != null && image.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(image.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/ImgVenues"), fileName);
                    image.SaveAs(path);
                    venueToUpdate.Photo = fileName;
                }

                // Se non viene fornita una nuova immagine, conserva quella esistente
                else
                {
                    venueToUpdate.Photo = venueToUpdate.Photo;
                }

                db.Entry(venueToUpdate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(venues);
        }



        // GET: Venues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venues venues = db.Venues.Find(id);
            if (venues == null)
            {
                return HttpNotFound();
            }
            return View(venues);
        }

        // POST: Venues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var venue = db.Venues.Include(v => v.Events).FirstOrDefault(v => v.VenueID == id);

            // Controlla se il luogo è associato a un evento
            if (venue != null && venue.Events.Any())
            {
                // Imposta il messaggio di errore
                TempData["ErrorMessage"] = "Non è possibile eliminare l'arena perché è associata a uno o più eventi.";
                return RedirectToAction("Delete", new { id = id });
            }

            db.Venues.Remove(venue);
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
