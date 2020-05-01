using Microsoft.EntityFrameworkCore;
using Pointwise.Domain.Interfaces;
using Pointwise.Domain.Models;
using Pointwise.Domain.Repositories;
using Pointwise.SqlDataAccess.ModelExtensions;
using Pointwise.SqlDataAccess.SQLContext;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pointwise.SqlDataAccess.SqlRepositories
{
    public sealed class SqlUserRepository : IUserRepository, IDisposable
    {
        private readonly PointwiseSqlContext context;

        public SqlUserRepository(DbContextOptions<PointwiseSqlContext> options)
        {
            context = new PointwiseSqlContext(options);

        }

        public IUser Add(Domain.Models.User entity)
        {
            var sEntity = entity.ToPersistentEntity();
            if (entity.UserType == null) sEntity.UserTypeId = 3;
            var insertedRow = context.Users.Add(sEntity);
            context.SaveChanges();

            return insertedRow.Entity.ToDomainEntity();
            //return entity;
        }

        public IEnumerable<IUser> AddRange(IEnumerable<Domain.Models.User> entities)
        {
            throw new NotImplementedException();
            //var sEntities = entities.Select(x => x.ToPersistentEntity()).AsEnumerable();
            //var insertedRows = context.Users.AddRange(sEntities);
            //context.SaveChanges();

            //return insertedRows.Select(x => x.ToDomainEntity()).AsEnumerable();
        }

        public bool SoftDelete(int id)
        {
            var sEntity = context.UserRoles.SingleOrDefault(x => x.Id == id);
            if (sEntity == null) return false;

            sEntity.IsDeleted = true;
            context.SaveChanges();
            return true;
        }

        public bool UndoSoftDelete(int id)
        {
            var sEntity = context.UserRoles.SingleOrDefault(x => x.Id == id);
            if (sEntity == null) return false;

            sEntity.IsDeleted = false;
            context.SaveChanges();
            return true;
        }

        public bool SoftDeleteRange(IEnumerable<Domain.Models.User> entities)
        {
            var sEntities = entities.Select(x => x.ToPersistentEntity()).AsEnumerable();
            foreach (var entity in sEntities)
            {
                entity.IsDeleted = true;
            }
            context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var sEntity = context.Users.SingleOrDefault(x => x.Id == id);
            if (sEntity == null) return false;

            context.Users.Remove(sEntity);
            context.SaveChanges();
            return true;
        }

        public bool DeleteRange(IEnumerable<Domain.Models.User> entities)
        {
            var sEntities = entities.Select(x => x.ToPersistentEntity()).AsEnumerable();
            context.Users.RemoveRange(sEntities);

            context.SaveChanges();
            return true;
        }

        public IUser Update(Domain.Models.User entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var sEntity = context.Users.Find(entity.Id);
            sEntity.FirstName = entity.FirstName;
            sEntity.MiddleName = entity.MiddleName;
            sEntity.LastName = entity.LastName;
            sEntity.EmailAddress = entity.EmailAddress;
            sEntity.PhoneNumber = entity.PhoneNumber;
            sEntity.UserType = entity.UserType;
            sEntity.UserNameType = entity.UserNameType;
            sEntity.UserName = entity.UserName;
            sEntity.Password = entity.Password;
            sEntity.IsBlocked = entity.IsBlocked;
            sEntity.LastModifiedOn = DateTime.Now;
            sEntity.CreatedBy = entity.CreatedBy;

            context.SaveChanges();
            return sEntity.ToDomainEntity();
        }

        public IEnumerable<IUser> GetAll()
        {
            var users = context.Users.AsEnumerable().Select(x => x.ToDomainEntity()).ToList();
            return users;
        }

        public IUser GetById(int id)
        {
            return context.Users.Find(id).ToDomainEntity();
        }

        public IUser Authenticate(string userName, string password)
        {
            var user = context.Users
                .Include(x => x.SqlUserType)
                .Include(x => x.SqlUserRoles)
                .SingleOrDefault(x => x.UserName == userName && x.Password == password);
            return user!= null? user.ToDomainEntity() : null;
        }

        public bool IsUnique(string userName)
        {
            var userExists = context.Users.SingleOrDefault(x => x.UserName == userName);
            return userExists == null;
        }

        public bool Logout(string userName)
        {
            var userExists = context.Users.SingleOrDefault(x => x.UserName == userName);
            return userExists != null;
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public bool Exist(string name)
        {
            throw new NotImplementedException();
        }
    }
}