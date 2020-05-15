using System;
using System.Collections.Generic;
using Pointwise.Domain.Interfaces;

namespace Pointwise.Domain.ServiceInterfaces
{
    public interface IUserService
    {
        IEnumerable<IUser> GetUsers();
        IUser GetById(int id);
        IEnumerable<IUser> GetUserByName(string nameString);
        IEnumerable<IUser> GetUserByEmailAddress(string emailString);
        IEnumerable<IUser> GetUserByPhoneNumber(string phoneString);
        IEnumerable<IUser> GetBlockedUsers();
        bool UserIsBlocked(IUser user);
        IUser Add(Models.User user);
        IUser Update(Models.User user);

        bool SoftDelete(int id);
        bool UndoSoftDelete(int id);

        bool Block(int id);
        bool Unblock(int id);

        bool IsUnique(string userName);
    }
}
