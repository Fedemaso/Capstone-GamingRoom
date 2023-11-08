using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using GamingRoom.Models;


namespace GamingRoom.Models
{
    public class Cart
    {
        [Key]
        public int CartID { get; set; }
        public int UserID { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual Users User { get; set; }
    }

    public class CartItem
    {
        [Key]
        public int CartItemID { get; set; }
        public int CartID { get; set; }
        public int EventID { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("CartID")]
        public virtual Cart Cart { get; set; }

        [ForeignKey("EventID")]
        public virtual Events Event { get; set; }

        [NotMapped] // Non mappato perché non esiste come colonna nel database
        public decimal Price => Event.TicketPrice; 

        [NotMapped] // Calcolato in base alla quantità e al prezzo per evento
        public decimal Total => Quantity * Price;
    }

}