using Pointwise.Domain.Interfaces;
using Pointwise.Domain.Models;
using Pointwise.Domain.Repositories;
using Pointwise.Domain.ServiceInterfaces;
using System;
using System.Collections.Generic;

namespace Pointwise.Domain.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository repository;

        public UserRoleService(IUserRoleRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public IEnumerable<IUserRole> GetUserRoles(int userId)
        {
            return repository.GetUserRoles(userId);
        }

        public IUserRole AddUserRole(UserRole entity)
        {
            return repository.AddUserRole(entity);
        }
    }
}