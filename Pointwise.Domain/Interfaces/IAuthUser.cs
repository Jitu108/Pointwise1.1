using System;

namespace Pointwise.Domain.Interfaces
{
    public interface IAuthUser : IUser
    {
        string Token { get; set; }
        DateTime ExpiryDate { get; set; }
    }
}
