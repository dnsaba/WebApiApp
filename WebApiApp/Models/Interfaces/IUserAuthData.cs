using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiApp.Models.Interfaces
{
    public interface IUserAuthData
    {
        int Id { get; }
        string Email { get; }
    }
}

