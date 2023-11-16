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
    // Questo attributo specifica che solo gli utenti con il ruolo "SuperAdmin" possono accedere a questo controller.
    public class EventTitlesController : Controller
    {
        private ModelDBContext db = new ModelDBContext();

        // GET: EventTitles
        // Questo metodo gestisce la richiesta GET per visualizzare una lista di associazioni tra eventi e titoli.
        public ActionResult Index()
        {
            var eventTitles = db.EventTitles.Include(e => e.Events).Include(e => e.Titles);
            return View(eventTitles.ToList());
        }

        // GET: EventTitles/Details/5
        // Questo metodo gestisce la richiesta GET per visualizzare i dettagli di un'associazione tra evento e titolo.
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventTitles eventTitles = db.EventTitles.Find(id);
            if (eventTitles == null)
            {
                return HttpNotFound();
            }
            return View(eventTitles);
        }

        // GET: EventTitles/Create
        // Questo metodo gestisce la richiesta GET per creare una nuova associazione tra evento e titolo.
        public ActionResult Create()
        {
            ViewBag.EventID = new SelectList(db.Events, "EventID", "Name");
            ViewBag.TitleID = new SelectList(db.Titles, "TitleID", "Name");
            return View();
        }

        // POST: EventTitles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Questo metodo gestisce la richiesta POST per creare una nuova associazione tra evento e titolo.
        public ActionResult Create([Bind(Include = "EventTitleID,EventID,TitleID")] EventTitles eventTitles)
        {
            if (ModelState.IsValid)
            {
                db.EventTitles.Add(eventTitles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EventID = new SelectList(db.Events, "EventID", "Name", eventTitles.EventID);
            ViewBag.TitleID = new SelectList(db.Titles, "TitleID", "Name", eventTitles.TitleID);
            return View(eventTitles);
        }

        // GET: EventTitles/Edit/5
        // Questo metodo gestisce la richiesta GET per modificare un'associazione tra evento e titolo.
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventTitles eventTitles = db.EventTitles.Find(id);
            if (eventTitles == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventID = new SelectList(db.Events, "EventID", "Name", eventTitles.EventID);
            ViewBag.TitleID = new SelectList(db.Titles, "TitleID", "Name", eventTitles.TitleID);
            return View(eventTitles);
        }

        // POST: EventTitles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Questo metodo gestisce la richiesta POST per modificare un'associazione tra evento e titolo.
        public ActionResult Edit([Bind(Include = "EventTitleID,EventID,TitleID")] EventTitles eventTitles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eventTitles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventID = new SelectList(db.Events, "EventID", "Name", eventTitles.EventID);
            ViewBag.TitleID = new SelectList(db.Titles, "TitleID", "Name", eventTitles.TitleID);
            return View(eventTitles);
        }

        // GET: EventTitles/Delete/5
        // Questo metodo gestisce la richiesta GET per eliminare un'associazione tra evento e titolo.
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventTitles eventTitles = db.EventTitles.Find(id);
            if (eventTitles == null)
            {
                return HttpNotFound();
            }
            return View(eventTitles);
        }

        // POST: EventTitles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        // Questo metodo gestisce la richiesta POST per confermare l'eliminazione di un'associazione tra evento e titolo.
        public ActionResult DeleteConfirmed(int id)
        {
            EventTitles eventTitles = db.EventTitles.Find(id);
            db.EventTitles.Remove(eventTitles);
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
