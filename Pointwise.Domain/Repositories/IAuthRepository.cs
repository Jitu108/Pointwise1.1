using Pointwise.Domain.Interfaces;

namespace Pointwise.Domain.Repositories
{
    public interface IAuthRepository
    {
        IAuthUser Authenticate(string userName, string password);
        bool Logout(string userName);
    }
}
