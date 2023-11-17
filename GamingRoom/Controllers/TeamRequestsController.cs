using GamingRoom.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GamingRoom.Controllers
{
    public class TeamRequestsController : Controller
    {
        private ModelDBContext db = new ModelDBContext();

        // Metodo per creare una nuova richiesta di team
        public ActionResult Create()
        {
            ViewBag.CreatedBy = GetUsersList(); // Ottieni la lista degli utenti per il dropdown

            return View();
        }

        // Metodo per creare la lista di SelectListItem
        private IEnumerable<SelectListItem> GetUsersList()
        {
            return db.Users.Select(u => new SelectListItem
            {
                Value = u.UserID.ToString(),
                Text = u.Username 
            }).ToList();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Description,CreatedBy")] TeamRequest teamRequest, HttpPostedFileBase proposedPhoto)
        {
            if (ModelState.IsValid)
            {
                if (proposedPhoto != null && proposedPhoto.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(proposedPhoto.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/ImgTeams"), fileName);
                    proposedPhoto.SaveAs(path);
                    teamRequest.ProposedPhoto = fileName; // Salva il nome del file nel database
                }

                db.TeamRequests.Add(teamRequest);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            ViewBag.CreatedBy = GetUsersList(); // Mantieni il dropdown in caso di errore
            return View(teamRequest);
        }

        [Authorize(Roles = "SuperAdmin")]
        public ActionResult Index()
        {
            return View(db.TeamRequests.ToList());
        }

        // Metodo per approvare una richiesta di team
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult Approve(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TeamRequest teamRequest = db.TeamRequests.Find(id);
            if (teamRequest == null)
            {
                return HttpNotFound();
            }

            Teams newTeam = new Teams
            {
                Name = teamRequest.Name,
                Description = teamRequest.Description,
                Photo = teamRequest.ProposedPhoto,
                CreatedBy = teamRequest.CreatedBy
            };

            db.Teams.Add(newTeam);
            db.TeamRequests.Remove(teamRequest);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // Metodo per eliminare una richiesta di team
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamRequest teamRequest = db.TeamRequests.Find(id);
            if (teamRequest == null)
            {
                return HttpNotFound();
            }
            return View(teamRequest);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult DeleteConfirmed(int id)
        {
            TeamRequest teamRequest = db.TeamRequests.Find(id);
            db.TeamRequests.Remove(teamRequest);
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
