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

        public IEnumerable<IUserRole> GetUserRoles()
        {
            return repository.GetAll();
        }

        public IUserRole GetById(int id)
        {
            return repository.GetById(id);
        }

        public IUserRole Add(UserRole entity)
        {
            return repository.Add(entity);
        }

        public bool Delete(int id)
        {
            return repository.Delete(id);
        }

        public bool SoftDelete(int id)
        {
            return repository.SoftDelete(id);
        }

        public bool UndoSoftDelete(int id)
        {
            return repository.UndoSoftDelete(id);
        }

        public IUserRole Update(UserRole entity)
        {
            return repository.Update(entity);
        }
    }
}