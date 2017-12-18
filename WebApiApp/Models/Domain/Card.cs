using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiApp.Models.Domain
{
    public class Card
    {
        public int Id { get; set; }
        public int FileId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
    }
}