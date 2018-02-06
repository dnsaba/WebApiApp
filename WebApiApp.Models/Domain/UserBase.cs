using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiApp.Models.Interfaces;

namespace WebApiApp.Models.Domain
{
    public class UserBase : IUserAuthData
    {
        public int Id
        {
            get; set;
        }

        public string Email
        {
            get; set;
        }

        public IEnumerable<string> Roles
        {
            get; set;
        }

        public bool Remember { get; set; }

        public string UserType { get; set; }

        public string RoleId { get; set; }
    }
}