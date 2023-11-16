
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

    public class TitlesController : Controller
    {
        private ModelDBContext db = new ModelDBContext();

        // GET: Titles - Mostra l'elenco dei titoli
        public ActionResult Index()
        {
            return View(db.Titles.ToList());
        }

        // GET: Titles/Details/5 - Mostra i dettagli di un titolo specifico
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

        // GET: Titles/Create - Mostra il form per la creazione di un nuovo titolo
        public ActionResult Create()
        {
            return View();
        }

        // POST: Titles/Create - Gestisce la creazione di un nuovo titolo
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

        // GET: Titles/Edit/5 - Mostra il form per la modifica di un titolo esistente
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

        // POST: Titles/Edit/5 - Gestisce la modifica di un titolo esistente
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

        // GET: Titles/Delete/5 - Mostra il form per la cancellazione di un titolo
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

        // POST: Titles/Delete/5 - Gestisce la cancellazione di un titolo
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
