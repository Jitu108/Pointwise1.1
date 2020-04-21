using Pointwise.Domain.Interfaces;
using System.Collections.Generic;

namespace Pointwise.Domain.Repositories
{
    public interface IUserEmailRepository
    {
        IEnumerable<IUserEmail> GetAllUserEmails(int userId);
        IEnumerable<IUserEmail> GetUserPrimaryEmail(int userId);
    }
}
