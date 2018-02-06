using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiApp.Models.Domain
{
    public class RegisterUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string BasicPass { get; set; }
        public string Salt { get; set; }
        public string EncryptedPass { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}