using System;
using System.Collections.Generic;
using System.Linq;
using Pointwise.Domain.Interfaces;
using Pointwise.Domain.Models;
using Pointwise.Domain.Repositories;
using Pointwise.Domain.ServiceInterfaces;

namespace Pointwise.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository repository;

        public UserService(IUserRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public IEnumerable<IUser> GetUsers()
        {
            return repository.GetAll();
        }
        public IUser GetById(int id)
        {
            return repository.GetById(id);
        }
        public IEnumerable<IUser> GetUserByName(string nameString)
        {
            return repository.GetAll().Where(x => x.FirstName.Contains(nameString) || x.MiddleName.Contains(nameString) || x.LastName.Contains(nameString)); 
        }
        public IEnumerable<IUser> GetUserByEmailAddress(string emailString)
        {
            return repository.GetAll().Where(x => x.EmailAddress.Contains(emailString));
        }
        public IEnumerable<IUser> GetUserByPhoneNumber(string phoneString)
        {
            return repository.GetAll().Where(x => x.PhoneNumber.Contains(phoneString));
        }
        public IEnumerable<IUser> GetBlockedUsers()
        {
            return repository.GetAll().Where(x => x.IsBlocked);
        }
        public bool UserIsBlocked(IUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            return repository.GetById(user.Id).IsBlocked;
        }

        public IUser Add(User user)
        {
            return repository.Add(user);
        }

        public IUser Update(User user)
        {
            return repository.Update(user);
        }
        
        public bool IsUnique(string userName)
        {
            return repository.IsUnique(userName);
        }
    }
}
