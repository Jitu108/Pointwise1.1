using Pointwise.Domain.Interfaces;
using System.Collections.Generic;

namespace Pointwise.Domain.ServiceInterfaces
{
    public interface IUserRoleService
    {
        IEnumerable<IUserRole> GetUserRoles(int userId);
    }
}
