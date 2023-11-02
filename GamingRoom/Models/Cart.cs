using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public virtual Cart Cart { get; set; }
        public virtual Events Event { get; set; }
    }

}