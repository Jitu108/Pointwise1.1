using Pointwise.Domain.Interfaces;
using Pointwise.Domain.Models;
using System.Collections.Generic;

namespace Pointwise.Domain.Repositories
{
    public interface IUserRepository : IRepository<IUser, User>
    {
        IUser Authenticate(string userName, string password);
        bool IsUnique(string userName);
        bool Logout(string userName);
    }
}
