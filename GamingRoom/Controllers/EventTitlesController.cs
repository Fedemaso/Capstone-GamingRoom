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

    public class EventTitlesController : Controller
    {
        private ModelDBContext db = new ModelDBContext();

        // GET: EventTitles
        public ActionResult Index()
        {
            var eventTitles = db.EventTitles.Include(e => e.Events).Include(e => e.Titles);
            return View(eventTitles.ToList());
        }

        // GET: EventTitles/Details/5
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
        public ActionResult Create()
        {
            ViewBag.EventID = new SelectList(db.Events, "EventID", "Name");
            ViewBag.TitleID = new SelectList(db.Titles, "TitleID", "Name");
            return View();
        }

        // POST: EventTitles/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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
