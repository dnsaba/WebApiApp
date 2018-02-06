using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiApp.Models.Domain
{
    public class CardWithFile
    {
        public int Id { get; set; }
        public int FileId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int AttackLevel { get; set; }
        public int DefenseLevel { get; set; }
        public string UserFileName { get; set; }
        public string SystemFileName { get; set; }
        public int FileUserId { get; set; }
        public string CardCombo { get; set; }
        public int CardComboAtk { get; set; }
        public int CardComboDef { get; set; }
    }
}