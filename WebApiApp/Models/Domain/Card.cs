using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiApp.Models.Domain
{
    public class Card
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int AttackLevel { get; set; }
        public int DefenseLevel { get; set; }
    }
}