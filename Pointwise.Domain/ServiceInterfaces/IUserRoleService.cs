using Pointwise.Domain.Interfaces;
using System.Collections.Generic;

namespace Pointwise.Domain.ServiceInterfaces
{
    public interface IUserRoleService
    {
        IEnumerable<IUserRole> GetUserRoles();
        IUserRole GetById(int id);

        IUserRole Add(Models.UserRole entity);

        bool Delete(int id);

        bool SoftDelete(int id);
        bool UndoSoftDelete(int id);

        IUserRole Update(Models.UserRole entity);
    }
}
