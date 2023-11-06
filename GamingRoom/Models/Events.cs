namespace GamingRoom.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Events
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Events()
        {
            EventTitles = new HashSet<EventTitles>();
            Tickets = new HashSet<Tickets>();
        }

        [Key]
        public int EventID { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public string Description { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        public int? VenueID { get; set; }
        public string Photo { get; set; }


        public int? TicketsAvailable { get; set; }


        public int? TicketsSold { get; set; }

        public bool IsActive { get; set; }

        public int? CreatedBy { get; set; }

        public virtual Users Users { get; set; }

        public virtual ICollection<Teams> Teams { get; set; }



        public virtual Venues Venues { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventTitles> EventTitles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tickets> Tickets { get; set; }
    }
}
