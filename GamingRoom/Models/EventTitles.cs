namespace GamingRoom.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EventTitles
    {
        [Key]
        public int EventTitleID { get; set; }

        public int? EventID { get; set; }

        public int? TitleID { get; set; }

        public virtual Events Events { get; set; }

        public virtual Titles Titles { get; set; }
    }
}
