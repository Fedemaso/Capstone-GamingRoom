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
    public class TitlesController : Controller
    {
        private ModelDBContext db = new ModelDBContext();

        // GET: Titles
        public ActionResult Index()
        {
            return View(db.Titles.ToList());
        }

        // GET: Titles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Titles titles = db.Titles.Find(id);
            if (titles == null)
            {
                return HttpNotFound();
            }
            return View(titles);
        }

        // GET: Titles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Titles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TitleID,Name,Description,ProductionHouse,Photo")] Titles titles, HttpPostedFileBase titlePhoto)
        {
            if (ModelState.IsValid)
            {
                if (titlePhoto != null && titlePhoto.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(titlePhoto.FileName);
                    var pathToSave = Path.Combine(Server.MapPath("~/Content/ImgTitles/"), fileName);
                    titlePhoto.SaveAs(pathToSave);
                    titles.Photo = fileName;
                }

                db.Titles.Add(titles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(titles);
        }

        // GET: Titles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Titles titles = db.Titles.Find(id);
            if (titles == null)
            {
                return HttpNotFound();
            }
            return View(titles);
        }

        // POST: Titles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TitleID,Name,Description,ProductionHouse,Photo")] Titles titles, HttpPostedFileBase titlePhoto)
        {
            if (ModelState.IsValid)
            {
                if (titlePhoto != null && titlePhoto.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(titlePhoto.FileName);
                    var pathToSave = Path.Combine(Server.MapPath("~/Content/ImgTitles/"), fileName);
                    titlePhoto.SaveAs(pathToSave);
                    titles.Photo = fileName;
                }

                db.Entry(titles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(titles);
        }


        // GET: Titles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Titles titles = db.Titles.Find(id);
            if (titles == null)
            {
                return HttpNotFound();
            }
            return View(titles);
        }

        // POST: Titles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Titles titles = db.Titles.Find(id);
            db.Titles.Remove(titles);
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
