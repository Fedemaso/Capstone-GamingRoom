using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GamingRoom.Models
{
    public class CarrelloItem
    {
        public int Id { get; set; }
        public string NomeEvento { get; set; }
        public int Quantita { get; set; }
        public decimal Prezzo { get; set; }
        public decimal Totale { get { return Quantita * Prezzo; } }
    }
}
