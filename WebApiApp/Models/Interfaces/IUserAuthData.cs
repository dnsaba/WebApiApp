using System.Collections.Generic;

namespace WebApiApp.Models.Interfaces
{
    public interface IUserAuthData
    {
        int Id { get; }
        string Email { get; }
        IEnumerable<string> Roles { get; }
        bool Remember { get; }
        string UserType { get; }
        string RoleId { get; }
    }
}

