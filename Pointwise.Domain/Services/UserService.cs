using System;
using System.Collections.Generic;
using System.Linq;
using Pointwise.Domain.Enums;
using Pointwise.Domain.Interfaces;
using Pointwise.Domain.Models;
using Pointwise.Domain.Repositories;
using Pointwise.Domain.ServiceInterfaces;

namespace Pointwise.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepo;
        private readonly IUserRoleRepository userRoleRepo;
        private readonly IUserTypeRepository userTypeRepo;

        public UserService(IUserRepository userRepo, IUserRoleRepository userRoleRepo, IUserTypeRepository userTypeRepo)
        {
            this.userRepo = userRepo ?? throw new ArgumentNullException(nameof(userRepo));
            this.userRoleRepo = userRoleRepo;
            this.userTypeRepo = userTypeRepo;
        }
        public IEnumerable<IUser> GetUsers()
        {
            return userRepo.GetAll();
        }
        public IUser GetById(int id)
        {
            return userRepo.GetById(id);
        }
        public IEnumerable<IUser> GetUserByName(string nameString)
        {
            return userRepo.GetAll().Where(x => x.FirstName.Contains(nameString) || x.MiddleName.Contains(nameString) || x.LastName.Contains(nameString)); 
        }
        public IEnumerable<IUser> GetUserByEmailAddress(string emailString)
        {
            return userRepo.GetAll().Where(x => x.EmailAddress.Contains(emailString));
        }
        public IEnumerable<IUser> GetUserByPhoneNumber(string phoneString)
        {
            return userRepo.GetAll().Where(x => x.PhoneNumber.Contains(phoneString));
        }
        public IEnumerable<IUser> GetBlockedUsers()
        {
            return userRepo.GetAll().Where(x => x.IsBlocked);
        }
        public bool UserIsBlocked(IUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            return userRepo.GetById(user.Id).IsBlocked;
        }

        public IUser Add(User user)
        {
            var addedUser = userRepo.Add(user);
            var addedUserRole = userRoleRepo.AddUserRole(new UserRole { User = addedUser, EntityType = EntityType.Article, AccessType = AccessType.Select });
            return addedUser;
        }

        public IUser Update(User user)
        {
            user.UserType = this.userTypeRepo.GetByName(user.UserType.Name);
            userRepo.Update(user);

            var existingRoles = userRoleRepo.GetUserRoles(user.Id).Select(x => (UserRole)x).ToList();
            var newRoles = user.Roles.Select(x => (UserRole)x).ToList(); ;
            newRoles.ForEach(x =>
            {
                x.CreatedBy = user.CreatedBy.Value;
                x.User = user;
            });


            // Roles To Remove
            var rolesToRemove = existingRoles.ToList();
            foreach (var entity in existingRoles.ToList())
            {
                var t = entity;
                if (newRoles.Any(x =>
                 x.User.Id == t.User.Id
                 && x.EntityType == t.EntityType
                 && x.AccessType == t.AccessType))
                {
                    rolesToRemove.Remove(t);
                }
            }

            // Roles To Add
            var rolesToAdd = newRoles.ToList();
            foreach (var entity in newRoles.ToList())
            {
                var t = entity;
                if (existingRoles.Any(x =>
                 x.User.Id == t.User.Id
                 && x.EntityType == t.EntityType
                 && x.AccessType == t.AccessType))
                {
                    rolesToAdd.Remove(t);
                }
            }
            userRoleRepo.RemoveUserRole(rolesToRemove);
            userRoleRepo.AddUserRole(rolesToAdd);
            return GetById(user.Id);
        }

        public bool SoftDelete(int id)
        {
            return userRepo.SoftDelete(id);
        }

        public bool UndoSoftDelete(int id)
        {
            return userRepo.UndoSoftDelete(id);
        }

        public bool IsUnique(string userName)
        {
            return userRepo.IsUnique(userName);
        }

        public bool Block(int id)
        {
            return userRepo.Block(id);
        }

        public bool Unblock(int id)
        {
            return userRepo.Unblock(id);
        }
    }
}
