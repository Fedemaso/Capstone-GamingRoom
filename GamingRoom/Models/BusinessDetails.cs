namespace GamingRoom.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BusinessDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BusinessID { get; set; }

        [Required]
        [StringLength(255)]
        public string BusinessName { get; set; }

        [StringLength(500)]
        public string Address { get; set; }

        [StringLength(50)]
        public string PhoneNumber { get; set; }

        [StringLength(255)]
        public string Website { get; set; }

        public virtual Users Users { get; set; }
    }
}
