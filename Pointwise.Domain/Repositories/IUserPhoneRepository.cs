using Pointwise.Domain.Interfaces;
using System.Collections.Generic;

namespace Pointwise.Domain.Repositories
{
    public interface IUserPhoneRepository
    {
        IEnumerable<IUserPhone> GetAllUserPhones(int userId);
        IEnumerable<IUserPhone> GetUserPrimaryPhone(int userId);
    }
}
