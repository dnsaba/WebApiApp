using System;

namespace WebApiApp.Models.Domain
{
    public class Person
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
        public int Won { get; set; }
        public int Lost { get; set; }
        public int TotalGames { get; set; }
        public string DisplayName { get; set; }
    }
}