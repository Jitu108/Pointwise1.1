using Pointwise.Domain.Interfaces;

namespace Pointwise.Domain.ServiceInterfaces
{
    public interface IAuthService
    {
        IAuthUser Authenticate(string userName, string password, byte[] key);
        bool Logout(string userName);
    }
}
