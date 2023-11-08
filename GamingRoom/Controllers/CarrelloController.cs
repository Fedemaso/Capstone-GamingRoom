using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GamingRoom.Models;

namespace GamingRoom.Controllers
{
    public class CartController : Controller
    {
        private ModelDBContext db = new ModelDBContext();

        // Metodo per aggiornare la sessione del carrello
        private void UpdateCartSession(List<CartItem> cartItems)
        {
            Session["Cart"] = cartItems; // Aggiorna la sessione con la lista aggiornata
        }

        // Metodo per ottenere il carrello dalla sessione
        private List<CartItem> GetCartItems()
        {
            var cart = Session["Cart"] as List<CartItem>;
            return cart ?? new List<CartItem>(); // Ritorna una nuova lista se la sessione è null
        }







        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult AddToCart(int eventId, int quantity, decimal ticketPrice, string eventName)
        {
            // Verifica se l'evento esiste
            var eventItem = db.Events.Find(eventId);
            if (eventItem == null)
            {
                // Gestisci il caso in cui l'evento non esiste
                return HttpNotFound();
            }

            // Ottieni il carrello corrente dalla sessione o creane uno nuovo se non esiste
            var cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();

            // Cerca se l'articolo è già nel carrello
            var cartItem = cart.FirstOrDefault(item => item.EventID == eventId);

            if (cartItem == null)
            {
                // Se l'articolo non è nel carrello, aggiungilo
                cart.Add(new CartItem
                {
                    EventID = eventId,
                    Quantity = quantity,
                    Cart = new Cart 
                    {
                        // Inizializza le proprietà del Cart se necessario
                    },
                    Event = new Events 
                    {
                        EventID = eventId,
                        Name = eventName,
                        TicketPrice = ticketPrice
                    }
                });
            }
            else
            {
                // Se l'articolo è già nel carrello, aggiorna solo la quantità
                cartItem.Quantity += quantity;
            }

            Session["Cart"] = cart;

            return PartialView("_CartModal", cart);
        }







        // Rimuovi dal carrello

        [HttpPost]
        public ActionResult RemoveFromCart(int cartItemId)
        {
            var cartItems = GetCartItems();
            var cartItemToRemove = cartItems.SingleOrDefault(c => c.CartItemID == cartItemId); 

            if (cartItemToRemove != null)
            {
                cartItems.Remove(cartItemToRemove); 
            }

            UpdateCartSession(cartItems);

            return PartialView("_CartModal", cartItems);

        }





        // Svuota carrello
        [HttpPost]
        public ActionResult EmptyCart()
        {
            UpdateCartSession(new List<CartItem>()); // Svuota il carrello

            return PartialView("_CartModal", new List<CartItem>());
        }




        // Visualizza il riepilogo del carrello nel modale
        public ActionResult CartSummary()
        {
            var cartItems = GetCartItems(); // Ottieni gli articoli del carrello dalla sessione

            return PartialView("_CartModal", cartItems); 
        }




        // Checkout
        // ...

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
