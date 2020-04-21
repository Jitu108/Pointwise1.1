using System;
using System.Collections.Generic;
using Pointwise.Domain.Interfaces;

namespace Pointwise.Domain.ServiceInterfaces
{
    public interface IUserEmailService
    {
        IEnumerable<IUserEmail> GetAllUserEmails(int userId);
        IEnumerable<IUserEmail> GetUserPrimaryEmail(int userId);
    }
}
