using System;
using System.Collections.Generic;
using Pointwise.Domain.Interfaces;

namespace Pointwise.Domain.ServiceInterfaces
{
    public interface IUserPhoneService
    {
        IEnumerable<IUserPhone> GetAllUserPhones(int userId);
        IEnumerable<IUserPhone> GetUserPrimaryPhone(int userId);
    }
}
