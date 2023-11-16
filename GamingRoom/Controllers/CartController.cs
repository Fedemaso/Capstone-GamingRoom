using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GamingRoom.Models;
using System.Data.Entity;


namespace GamingRoom.Controllers
{
    namespace GamingRoom.Controllers
    {
        public class CartController : Controller
        {
            private ModelDBContext db = new ModelDBContext();

            // GET: Cart
            // Questo metodo gestisce la richiesta GET per visualizzare il carrello degli acquisti.
            public ActionResult Index()
            {
                // Ottiene o crea un carrello utilizzando la sessione utente.
                var cart = GetOrCreateCart();

                // Se il carrello non esiste, lo crea e lo memorizza nella sessione.
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
                // Verifica se l'utente è autenticato; in caso contrario, reindirizza alla pagina di accesso.
                if (!User.Identity.IsAuthenticated)
                {
                    Session["RedirectEventID"] = EventID;
                    Session["RedirectQuantity"] = quantity;
                    return RedirectToAction("Login", "Login");
                }

                var cart = GetOrCreateCart();
                var item = cart.CartItems.FirstOrDefault(i => i.EventID == EventID);

                // Aggiunge un elemento al carrello o aggiorna la quantità se l'evento è già nel carrello.
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

                // Aggiorna la quantità dell'elemento nel carrello o lo rimuove se la quantità è zero.
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
                // Rimuove il carrello dalla sessione.
                Session.Remove("Cart");
                return RedirectToAction("Index");
            }

            // GET: Cart/Checkout
            // Questo metodo gestisce la richiesta GET per il checkout del carrello.
            public ActionResult Checkout()
            {
                var cart = GetOrCreateCart();

                // Se il carrello è vuoto, reindirizza alla pagina principale del carrello.
                if (cart.CartItems.Count == 0)
                {
                    return RedirectToAction("Index");
                }

                // Recupera i dettagli dell'utente e li passa alla vista del checkout.
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

                // Verifica se l'utente esiste e se il carrello contiene almeno un elemento.
                if (user == null || !cart.CartItems.Any())
                {
                    return RedirectToAction("Index");
                }

                // Crea un ordine e aggiunge dettagli dell'ordine al database.
                var order = new Order { UserId = user.UserId, OrderDate = DateTime.Now, Total = cart.CartItems.Sum(i => i.Total) };
                db.Orders.Add(order);
                db.SaveChanges();

                foreach (var item in cart.CartItems)
                {
                    var orderDetail = new OrderDetail { OrderId = order.OrderId, EventID = item.EventID, Quantity = item.Quantity, UnitPrice = item.Price };
                    db.OrderDetails.Add(orderDetail);
                }

                db.SaveChanges();
                Session["Cart"] = null; // Cancella il carrello dalla sessione dopo l'ordine.

                return RedirectToAction("OrderConfirmation");
            }

            public ActionResult OrderConfirmation()
            {
                // Mostra una conferma dell'ordine.
                return View();
            }
        }
    }

}
