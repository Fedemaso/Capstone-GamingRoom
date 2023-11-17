using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GamingRoom.Models
{
    public class EventRequest
    {
        [Key]
        public int EventRequestId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime ProposedDate { get; set; }

        public string VenueProposal { get; set; }

        public string ImageFileName { get; set; }
        public int ProposedTicketsAvailable { get; set; }

        [DataType(DataType.Currency)]
        public decimal ProposedTicketPrice { get; set; }

    }
}