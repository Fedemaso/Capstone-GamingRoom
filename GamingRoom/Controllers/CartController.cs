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
                cart = new Cart(); // Crea un nuovo carrello se non esiste
                cart.CartItems = new List<CartItem>(); // Assicurati che la lista degli articoli non sia mai null
                Session["Cart"] = cart;
            }

            return View(cart); // Ora puoi essere sicuro che CartItems non sia mai null
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
            Cart cart = (Cart)Session["Cart"] ?? new Cart { CartItems = new List<CartItem>() };
            CartItem item = cart.CartItems.FirstOrDefault(i => i.EventID == EventID);

            if (item == null)
            {
                // Carica l'evento completo dal database
                var eventToAdd = db.Events.Include(e => e.Venues).FirstOrDefault(e => e.EventID == EventID);
                if (eventToAdd != null) // Assicurati che l'evento esista
                {
                    cart.CartItems.Add(new CartItem
                    {
                        EventID = eventToAdd.EventID,
                        Quantity = quantity,
                        Event = eventToAdd // L'oggetto Event ora contiene tutti i dettagli necessari
                    });
                }
            }
            else
            {
                // Aggiorna solo la quantità per l'articolo esistente nel carrello
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

            // Recupera i dettagli dell'utente
            var user = db.UserCustomers.FirstOrDefault(u => u.Email == User.Identity.Name);
            if (user == null)
            {
                // Redirect l'utente alla pagina di login o mostra un errore
                return RedirectToAction("Login", "Account"); // Assicurati di avere un controller Account con azione Login
            }

            // Passa i dettagli all'utente attraverso ViewBag
            ViewBag.UserDetails = user;

            return View(cart);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmOrder()
        {
            var cart = GetOrCreateCart();
            var user = db.UserCustomers.FirstOrDefault(u => u.Email == User.Identity.Name); // Assumi che l'utente sia già autenticato e il suo Email sia univoco

            if (user == null || !cart.CartItems.Any())
            {
                // Gestisci l'errore qui
                return RedirectToAction("Index");
            }

            // Crea l'ordine
            Order order = new Order
            {
                UserId = user.UserId,
                OrderDate = DateTime.Now,
                Total = cart.CartItems.Sum(i => i.Total)
            };

            db.Orders.Add(order);
            db.SaveChanges(); // Salva l'ordine per ottenere un OrderId generato

            // Aggiungi i dettagli dell'ordine
            foreach (var item in cart.CartItems)
            {
                var orderDetail = new OrderDetail
                {
                    OrderId = order.OrderId,
                    EventID = item.EventID,
                    Quantity = item.Quantity,
                    UnitPrice = item.Price
                };
                db.OrderDetails.Add(orderDetail);
            }

            db.SaveChanges(); // Salva i dettagli dell'ordine

            // Pulisci il carrello
            Session["Cart"] = null;

            return RedirectToAction("OrderConfirmation"); // Crea questa view per mostrare la conferma
        }


        // GET: Cart/OrderConfirmation
        public ActionResult OrderConfirmation()
        {
            // Mostra una pagina di conferma dell'ordine qui
            return View();
        }

    }
}
