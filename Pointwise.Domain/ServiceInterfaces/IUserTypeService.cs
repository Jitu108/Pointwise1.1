using Pointwise.Domain.Interfaces;
using System.Collections.Generic;

namespace Pointwise.Domain.ServiceInterfaces
{
    public interface IUserTypeService
    {
        IEnumerable<IUserType> GetUserTypes();
        IUserType GetById(int id);

        IUserType Add(Models.UserType entity);


        bool Delete(int id);

        bool SoftDelete(int id);
        bool UndoSoftDelete(int id);

        IUserType Update(Models.UserType entity);
    }
}
