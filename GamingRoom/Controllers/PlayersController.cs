using GamingRoom.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GamingRoom.Controllers
{
    public class PlayersController : Controller
    {
        private ModelDBContext db = new ModelDBContext();

        // GET: Players/Index
        // Questo metodo restituisce la lista di tutti i giocatori presenti nel database.
        public ActionResult Index()
        {
            var players = db.Players.Include(p => p.Teams).Include(p => p.Users);
            return View(players.ToList());
        }

        // GET: Players/Details/5
        // Questo metodo restituisce i dettagli di un giocatore specifico identificato dall'ID.
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Players player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // GET: Players/Create
        [Authorize(Roles = "SuperAdmin")]
        // Questo metodo gestisce la richiesta GET per creare un nuovo giocatore.
        public ActionResult Create()
        {
            ViewBag.TeamID = new SelectList(db.Teams, "TeamID", "Name");
            ViewBag.CreatedBy = new SelectList(db.Users, "UserID", "Username");
            return View();
        }

        // POST: Players/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        // Questo metodo gestisce la richiesta POST per creare un nuovo giocatore.
        public ActionResult Create(Players player, HttpPostedFileBase foto)
        {
            if (ModelState.IsValid)
            {
                if (foto != null && foto.ContentLength > 0)
                {
                    player.Photo = foto.FileName;
                    string pathToSave = Server.MapPath("~/Content/ImgPlayers/") + foto.FileName;
                    foto.SaveAs(pathToSave);
                }
                db.Players.Add(player);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TeamID = new SelectList(db.Teams, "TeamID", "Name", player.TeamID);
            ViewBag.CreatedBy = new SelectList(db.Users, "UserID", "Username", player.CreatedBy);
            return View(player);
        }

        // GET: Players/Edit/5
        [Authorize(Roles = "SuperAdmin")]
        // Questo metodo gestisce la richiesta GET per modificare un giocatore identificato dall'ID.
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Players player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            ViewBag.TeamID = new SelectList(db.Teams, "TeamID", "Name", player.TeamID);
            ViewBag.CreatedBy = new SelectList(db.Users, "UserID", "Username", player.CreatedBy);
            ViewBag.ExistingPhoto = player.Photo; // Mostra la foto esistente del giocatore
            return View(player);
        }

        // POST: Players/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        // Questo metodo gestisce la richiesta POST per modificare un giocatore identificato dall'ID.
        public ActionResult Edit([Bind(Include = "PlayerID,Name,Surname,Nickname,TeamID,CreatedBy,Photo")] Players player, HttpPostedFileBase playerPhoto)
        {
            if (ModelState.IsValid)
            {
                var playerToUpdate = db.Players.Find(player.PlayerID);

                // Aggiorna le proprietà del giocatore con i nuovi valori
                playerToUpdate.Name = player.Name;
                playerToUpdate.Surname = player.Surname;
                playerToUpdate.Nickname = player.Nickname;
                playerToUpdate.TeamID = player.TeamID;
                playerToUpdate.CreatedBy = player.CreatedBy;

                // Se viene fornita una nuova foto, salvala e aggiorna il record del giocatore
                if (playerPhoto != null && playerPhoto.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(playerPhoto.FileName);
                    var pathToSave = Path.Combine(Server.MapPath("~/Content/ImgPlayers/"), fileName);

                    // Salva la foto nel percorso specificato
                    playerPhoto.SaveAs(pathToSave);

                    // Aggiorna il campo Photo solo se la foto è stata salvata con successo
                    playerToUpdate.Photo = fileName;
                }

                db.Entry(playerToUpdate).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            // Se siamo arrivati fin qui, qualcosa non va, quindi ri-mostra il form
            ViewBag.TeamID = new SelectList(db.Teams, "TeamID", "Name", player.TeamID);
            ViewBag.CreatedBy = new SelectList(db.Users, "UserID", "Username", player.CreatedBy);
            return View(player);
        }

        // GET: Players/Delete/5
        [Authorize(Roles = "SuperAdmin")]
        // Questo metodo gestisce la richiesta GET per eliminare un giocatore identificato dall'ID.
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Players player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        // Questo metodo gestisce la richiesta POST per confermare l'eliminazione di un giocatore identificato dall'ID.
        public ActionResult DeleteConfirmed(int id)
        {
            Players player = db.Players.Find(id);
            db.Players.Remove(player);
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
