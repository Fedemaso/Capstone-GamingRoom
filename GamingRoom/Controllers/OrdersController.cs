using GamingRoom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GamingRoom.Controllers
{
    public class OrdersController : Controller
    {
        private ModelDBContext db = new ModelDBContext();

        // GET: Orders/Index
        // Questo metodo restituisce la lista di tutti gli ordini presenti nel database.
        public ActionResult Index()
        {
            var orders = db.Orders.Include("UserCustomer").Include("OrderDetails").Include("OrderDetails.Event").ToList();
            return View(orders);
        }

        // GET: Orders/Details/5
        // Questo metodo restituisce i dettagli di un ordine specifico identificato dall'ID.
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
        [Authorize(Roles = "SuperAdmin")]
        // Questo metodo gestisce la richiesta GET per eliminare un ordine identificato dall'ID.
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
        [Authorize(Roles = "SuperAdmin")]
        // Questo metodo gestisce la richiesta POST per confermare l'eliminazione di un ordine identificato dall'ID.
        public ActionResult DeleteConfirmed(int id)
        {
            var order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}


//// GET: Orders/Edit/5  PER FUTURO AGGIORNAMENTO
///



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


