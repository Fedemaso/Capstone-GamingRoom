using GamingRoom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GamingRoom.Controllers
{

    [Authorize(Roles = "SuperAdmin")]


    public class OrdersController : Controller
    {
        // GET: Orders
        private ModelDBContext db = new ModelDBContext();

        public ActionResult Index()
        {
            // Recupera tutti gli ordini dal database
            var orders = db.Orders.Include("UserCustomer").Include("OrderDetails").Include("OrderDetails.Event").ToList();
            return View(orders);
        }


        //// GET: Orders/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    var order = db.Orders.Find(id);
        //    if (order == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(order);
        //}

        //// POST: Orders/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(Order order)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(order).State = System.Data.Entity.EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(order);
        //}



        // GET: Orders/Details/5
        public ActionResult Details(int id)
        {
            var order = db.Orders.Include("UserCustomer").Include("OrderDetails").Include("OrderDetails.Event")
                                 .FirstOrDefault(o => o.OrderId == id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }





        // GET: Orders/Delete/5
        public ActionResult Delete(int id)
        {
            var order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}