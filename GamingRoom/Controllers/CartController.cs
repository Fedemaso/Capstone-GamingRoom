using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GamingRoom.Models;
using System.Data.Entity;


namespace GamingRoom.Controllers
{
    public class CartController : Controller
    {

        private ModelDBContext db = new ModelDBContext();


        // GET: Cart
        public ActionResult Index()
        {
            var cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart { CartItems = new List<CartItem>() };
                Session["Cart"] = cart;
            }
            return View(cart);
        }


        // Metodo helper per ottenere o creare un carrello
        private Cart GetOrCreateCart()
        {
            var cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart { CartItems = new List<CartItem>() };
                Session["Cart"] = cart;
            }
            return cart;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToCart(int EventID, int quantity)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Session["RedirectEventID"] = EventID;
                Session["RedirectQuantity"] = quantity;
                return RedirectToAction("Login", "Login");
            }

            var cart = GetOrCreateCart();
            var item = cart.CartItems.FirstOrDefault(i => i.EventID == EventID);
            if (item == null)
            {
                var eventToAdd = db.Events.Include(e => e.Venues).FirstOrDefault(e => e.EventID == EventID);
                if (eventToAdd != null)
                {
                    cart.CartItems.Add(new CartItem { EventID = eventToAdd.EventID, Quantity = quantity, Event = eventToAdd });
                }
            }
            else
            {
                item.Quantity += quantity;
            }
            Session["Cart"] = cart;
            return RedirectToAction("Index");
        }


        public ActionResult RemoveFromCart(int EventID)
        {
            var cart = GetOrCreateCart();
            var item = cart.CartItems.FirstOrDefault(i => i.EventID == EventID);

            if (item != null)
            {
                cart.CartItems.Remove(item);
                Session["Cart"] = cart;
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCart(int EventID, int quantity)
        {
            var cart = (Cart)Session["Cart"] ?? new Cart { CartItems = new List<CartItem>() };
            var itemToUpdate = cart.CartItems.FirstOrDefault(ci => ci.EventID == EventID);

            if (itemToUpdate != null && quantity > 0 && quantity <= itemToUpdate.Event.TicketsAvailable)
            {
                itemToUpdate.Quantity = quantity;
                Session["Cart"] = cart;
            }
            else if (itemToUpdate != null && quantity == 0)
            {
                cart.CartItems.Remove(itemToUpdate);
                Session["Cart"] = cart;
            }

            return RedirectToAction("Index");
        }


        public ActionResult ClearCart()
        {
            Session.Remove("Cart");
            return RedirectToAction("Index");
        }

        // GET: Cart/Checkout
        public ActionResult Checkout()
        {
            var cart = GetOrCreateCart();
            if (cart.CartItems.Count == 0)
            {
                return RedirectToAction("Index");
            }
            var user = db.UserCustomers.FirstOrDefault(u => u.Email == User.Identity.Name);
            ViewBag.UserDetails = user;
            return View(cart);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmOrder()
        {
            var cart = GetOrCreateCart();
            var user = db.UserCustomers.FirstOrDefault(u => u.Email == User.Identity.Name);

            if (user == null || !cart.CartItems.Any())
            {
                return RedirectToAction("Index");
            }

            var order = new Order { UserId = user.UserId, OrderDate = DateTime.Now, Total = cart.CartItems.Sum(i => i.Total) };
            db.Orders.Add(order);
            db.SaveChanges();

            foreach (var item in cart.CartItems)
            {
                var orderDetail = new OrderDetail { OrderId = order.OrderId, EventID = item.EventID, Quantity = item.Quantity, UnitPrice = item.Price };
                db.OrderDetails.Add(orderDetail);
            }
            db.SaveChanges();
            Session["Cart"] = null;

            return RedirectToAction("OrderConfirmation");
        }

        public ActionResult OrderConfirmation()
        {
            return View();
        }
    }

}
