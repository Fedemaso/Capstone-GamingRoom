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
    public class TeamsController : Controller
    {
        private ModelDBContext db = new ModelDBContext();

        // GET: Teams
        public ActionResult Index()
        {
            // Recupera tutti i team dal database e include gli utenti associati a ciascun team
            var teams = db.Teams.Include(t => t.Users);
            return View(teams.ToList());
        }

        // GET: Teams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teams teams = db.Teams.Find(id);
            if (teams == null)
            {
                return HttpNotFound();
            }
            return View(teams);
        }

        [Authorize(Roles = "SuperAdmin")]
        // GET: Teams/Create
        public ActionResult Create()
        {
            // Recupera l'elenco degli utenti per il dropdown e passalo alla vista
            ViewBag.CreatedBy = new SelectList(db.Users, "UserID", "Username");
            return View();
        }

        [Authorize(Roles = "SuperAdmin")]
        // POST: Teams/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeamID,Name,Description,CreatedBy")] Teams team, HttpPostedFileBase PhotoFile)
        {
            if (ModelState.IsValid)
            {
                // Salva la foto del team se fornita
                if (PhotoFile != null && PhotoFile.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(PhotoFile.FileName);
                    var pathToSave = Path.Combine(Server.MapPath("~/Content/ImgTeams/"), fileName);
                    PhotoFile.SaveAs(pathToSave);
                    team.Photo = fileName;
                }

                db.Teams.Add(team);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            // Se il modello non è valido, ripassa l'elenco degli utenti alla vista
            ViewBag.CreatedBy = new SelectList(db.Users, "UserID", "Username", team.CreatedBy);
            return View(team);
        }

        [Authorize(Roles = "SuperAdmin")]
        // GET: Teams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teams team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            // Passa l'elenco degli utenti e i dati del team alla vista
            ViewBag.CreatedBy = new SelectList(db.Users, "UserID", "Username", team.CreatedBy);
            return View(team);
        }

        [Authorize(Roles = "SuperAdmin")]
        // POST: Teams/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeamID,Name,Description,CreatedBy,Photo")] Teams team, HttpPostedFileBase PhotoFile)
        {
            if (ModelState.IsValid)
            {
                // Aggiorna la foto del team se fornita
                if (PhotoFile != null && PhotoFile.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(PhotoFile.FileName);
                    var pathToSave = Path.Combine(Server.MapPath("~/Content/ImgTeams/"), fileName);
                    PhotoFile.SaveAs(pathToSave);
                    team.Photo = fileName;
                }

                db.Entry(team).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            // Se il modello non è valido, ripassa l'elenco degli utenti e i dati del team alla vista
            ViewBag.CreatedBy = new SelectList(db.Users, "UserID", "Username", team.CreatedBy);
            return View(team);
        }

        [Authorize(Roles = "SuperAdmin")]
        // GET: Teams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teams teams = db.Teams.Find(id);
            if (teams == null)
            {
                return HttpNotFound();
            }
            return View(teams);
        }

        [Authorize(Roles = "SuperAdmin")]
        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Teams teams = db.Teams.Find(id);
            db.Teams.Remove(teams);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Visualizza i dettagli del team, inclusi i giocatori ed eventi associati
        public ActionResult UserTeamDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teams team = db.Teams
                           .Include(t => t.Players)
                           .Include(t => t.Events)
                           .SingleOrDefault(t => t.TeamID == id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // Visualizza tutti i team
        public ActionResult AllTeams()
        {
            // Recupera tutti i team inclusi i giocatori
            var teams = db.Teams
                          .Include(t => t.Players)
                          .ToList();

            return View(teams);
        }
    }
}
