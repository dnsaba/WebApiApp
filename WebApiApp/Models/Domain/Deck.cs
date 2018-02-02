using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiApp.Models.Domain
{
    public class Deck
    {
        public int Id { get; set; }
        public string DeckName { get; set; }
        public List<Card> CardList { get; set; }
    }
}