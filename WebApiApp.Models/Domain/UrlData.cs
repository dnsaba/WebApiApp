using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiApp.Models.Domain
{
    public class UrlData
    {
        public string Url { get; set; }
        public List<Card> CardsInfo { get; set; }
    }
}