﻿using System;
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

    public class BusinessDetailsController : Controller
    {
        private ModelDBContext db = new ModelDBContext();

        // GET: BusinessDetails
        
       
        public ActionResult Index()
        {
            var businessDetails = db.BusinessDetails.Include(b => b.Users);
            return View(businessDetails.ToList());
        }


       
        // GET: BusinessDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessDetails businessDetails = db.BusinessDetails.Find(id);
            if (businessDetails == null)
            {
                return HttpNotFound();
            }
            return View(businessDetails);
        }

       
        // GET: BusinessDetails/Create
        public ActionResult Create()
        {
            ViewBag.BusinessID = new SelectList(db.Users, "UserID", "Username");
            return View();
        }

        // POST: BusinessDetails/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BusinessID,BusinessName,Address,PhoneNumber,Website")] BusinessDetails businessDetails)
        {
            if (ModelState.IsValid)
            {
                db.BusinessDetails.Add(businessDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BusinessID = new SelectList(db.Users, "UserID", "Username", businessDetails.BusinessID);
            return View(businessDetails);
        }


       
        // GET: BusinessDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessDetails businessDetails = db.BusinessDetails.Find(id);
            if (businessDetails == null)
            {
                return HttpNotFound();
            }
            ViewBag.BusinessID = new SelectList(db.Users, "UserID", "Username", businessDetails.BusinessID);
            return View(businessDetails);
        }



        // POST: BusinessDetails/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BusinessID,BusinessName,Address,PhoneNumber,Website")] BusinessDetails businessDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(businessDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BusinessID = new SelectList(db.Users, "UserID", "Username", businessDetails.BusinessID);
            return View(businessDetails);
        }



        // GET: BusinessDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessDetails businessDetails = db.BusinessDetails.Find(id);
            if (businessDetails == null)
            {
                return HttpNotFound();
            }
            return View(businessDetails);
        }




        // POST: BusinessDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BusinessDetails businessDetails = db.BusinessDetails.Find(id);
            db.BusinessDetails.Remove(businessDetails);
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
