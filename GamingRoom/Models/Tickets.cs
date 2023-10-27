namespace GamingRoom.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tickets
    {
        [Key]
        public int TicketID { get; set; }

        public int? EventID { get; set; }

        public int? UserID { get; set; }

        public DateTime? PurchaseDate { get; set; }

        public decimal Price { get; set; }

        public virtual Events Events { get; set; }

        public virtual Users Users { get; set; }
    }
}
