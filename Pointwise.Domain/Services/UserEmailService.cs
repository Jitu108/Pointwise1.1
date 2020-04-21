using System;
using System.Collections.Generic;
using Pointwise.Domain.Interfaces;
using Pointwise.Domain.Repositories;
using Pointwise.Domain.ServiceInterfaces;

namespace Pointwise.Domain.Services
{
    public class UserEmailService : IUserEmailService
    {
        private readonly IUserEmailRepository repository;
        public UserEmailService(IUserEmailRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public IEnumerable<IUserEmail> GetAllUserEmails(int userId)
        {
            return repository.GetAllUserEmails(userId);
        }
        public IEnumerable<IUserEmail> GetUserPrimaryEmail(int userId)
        {
            return repository.GetUserPrimaryEmail(userId);
        }
    }
}
