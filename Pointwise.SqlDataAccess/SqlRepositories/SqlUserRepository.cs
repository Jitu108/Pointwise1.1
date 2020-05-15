using Microsoft.EntityFrameworkCore;
using Pointwise.Domain.Interfaces;
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

        public IEnumerable<IUser> GetAll()
        {
            var users = context.Users
                .Include(x => x.SqlUserType)
                .Include(x => x.SqlUserRoles)
                .AsEnumerable().Select(x => x.ToDomainEntity()).ToList();
            return users;
        }

        public IUser GetById(int id)
        {
            
            try
            {
                var user = context.Users
                .Include(x => x.SqlUserType)
                .Include(x => x.SqlUserRoles)
                .Where(x => x.Id == id)
                .FirstOrDefault();
                if(user != null)
                {
                    return user.ToDomainEntity();
                }
                else
                {
                    return null;
                }
                
            } catch(Exception ex)
            {
                throw ex;
            }
        }

        public IUser Add(Domain.Models.User entity)
        {
            var sEntity = entity.ToPersistentEntity();

            // TODO: Move it to Service
            if (entity.UserType == null || entity.UserType.Id == 0) sEntity.UserTypeId = 2;
            var insertedRow = context.Users.Add(sEntity);
            context.SaveChanges();

            return insertedRow.Entity.ToDomainEntity();
            //return entity;
        }

        public bool SoftDelete(int id)
        {
            var sEntity = context.Users.SingleOrDefault(x => x.Id == id);
            if (sEntity == null) return false;

            sEntity.IsDeleted = true;
            context.SaveChanges();
            return true;
        }

        public bool UndoSoftDelete(int id)
        {
            var sEntity = context.Users.SingleOrDefault(x => x.Id == id);
            if (sEntity == null) return false;

            sEntity.IsDeleted = false;
            context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
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
            sEntity.UserTypeId = entity.UserType.Id;
            sEntity.UserNameType = entity.UserNameType;
            sEntity.UserName = entity.UserName;
            sEntity.Password = entity.Password;
            sEntity.IsBlocked = entity.IsBlocked;
            sEntity.IsDeleted = entity.IsDeleted;

            sEntity.LastModifiedOn = DateTime.Now;
            

            context.SaveChanges();
            return sEntity.ToDomainEntity();
        }

        public bool IsUnique(string userName)
        {
            var userExists = context.Users.SingleOrDefault(x => x.UserName == userName);
            return userExists == null;
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public bool Exist(string name)
        {
            throw new NotImplementedException();
        }

        public bool Block(int id)
        {
            var sEntity = context.Users.SingleOrDefault(x => x.Id == id);
            if (sEntity == null) return false;

            sEntity.IsBlocked = true;
            context.SaveChanges();
            return true;
        }

        public bool Unblock(int id)
        {
            var sEntity = context.Users.SingleOrDefault(x => x.Id == id);
            if (sEntity == null) return false;

            sEntity.IsBlocked = false;
            context.SaveChanges();
            return true;
        }
    }
}