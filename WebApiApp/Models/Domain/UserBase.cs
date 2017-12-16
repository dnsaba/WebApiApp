using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiApp.Interfaces;

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
    }
}