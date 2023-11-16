using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GamingRoom.Models
{

    public class TeamRequest
    {
        [Key]
        public int TeamRequestId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public string Description { get; set; }
        public string ProposedPhoto { get; set; }

        public int? CreatedBy { get; set; }


    }
}