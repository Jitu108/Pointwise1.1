using Pointwise.Domain.Interfaces;
using Pointwise.Domain.Models;
using System.Collections.Generic;

namespace Pointwise.Domain.Repositories
{
    public interface IUserRoleRepository
    {
        IEnumerable<IUserRole> GetUserRoles(int userId);

    }
}
