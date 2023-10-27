namespace GamingRoom.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Players
    {
        [Key]
        public int PlayerID { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Surname { get; set; }

        [StringLength(255)]
        public string Nickname { get; set; }
        public string Photo { get; set; }


        public int? TeamID { get; set; }

        public int? CreatedBy { get; set; }

        public virtual Users Users { get; set; }

        public virtual Teams Teams { get; set; }
    }
}
