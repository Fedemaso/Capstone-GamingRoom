namespace GamingRoom.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Venues
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Venues()
        {
            Events = new HashSet<Events>();
        }

        [Key]
        public int VenueID { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public string Photo { get; set; }


        [Required]
        [StringLength(500)]
        public string Address { get; set; }

        public int? Capacity { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Events> Events { get; set; }
    }
}
