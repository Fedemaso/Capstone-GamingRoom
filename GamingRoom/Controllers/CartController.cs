using GamingRoom.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
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
    }
}
