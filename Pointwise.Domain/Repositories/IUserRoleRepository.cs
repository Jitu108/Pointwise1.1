using Pointwise.Domain.Interfaces;
using Pointwise.Domain.Models;
using System.Collections.Generic;

namespace Pointwise.Domain.Repositories
{
    public interface IUserRoleRepository
    {
        IEnumerable<IUserRole> GetUserRoles(int userId);
        IUserRole AddUserRole(UserRole entity);
        bool AddUserRole(IEnumerable<UserRole> entities);

        bool RemoveUserRole(UserRole entity);
        bool RemoveUserRole(IEnumerable<UserRole> entities);
    }
}
