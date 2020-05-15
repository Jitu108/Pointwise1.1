using Pointwise.Domain.Interfaces;
using Pointwise.Domain.Models;
using System.Collections.Generic;

namespace Pointwise.Domain.ServiceInterfaces
{
    public interface IUserRoleService
    {
        IEnumerable<IUserRole> GetUserRoles(int userId);

        IUserRole AddUserRole(UserRole entity);
    }
}
