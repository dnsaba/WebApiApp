using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApiApp.Models.Domain
{
    public class RegisterUser
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        [RegularExpression(@"[0-9]+",
            ErrorMessage = "Must contain at least one digit")]
        public string BasicPass { get; set; }

        public string Salt { get; set; }
        public string EncryptedPass { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}