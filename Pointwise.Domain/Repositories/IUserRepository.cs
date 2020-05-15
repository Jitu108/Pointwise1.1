using Pointwise.Domain.Interfaces;
using Pointwise.Domain.Models;

namespace Pointwise.Domain.Repositories
{
    public interface IUserRepository : IRepository<IUser, User>
    {
        bool IsUnique(string userName);
        bool Block(int id);
        bool Unblock(int id);
    }
}
