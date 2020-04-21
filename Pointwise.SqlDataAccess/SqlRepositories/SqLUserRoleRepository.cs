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
    public sealed class SqlUserRoleRepository : IUserRoleRepository, IDisposable
    {
        private readonly PointwiseSqlContext context;

        public SqlUserRoleRepository(DbContextOptions<PointwiseSqlContext> options)
        {
            context = new PointwiseSqlContext(options);

        }

        public IEnumerable<IUserRole> GetAll()
        {
            var userRoles = context.UserRoles.AsEnumerable().Select(x => x.ToDomainEntity()).ToList();
            return userRoles;
        }

        public IUserRole GetById(int id)
        {
            return context.UserRoles.Find(id).ToDomainEntity();
        }

        public IUserRole Add(Domain.Models.UserRole entity)
        {
            var sEntity = entity.ToPersistentEntity();
            var insertedRow = context.UserRoles.Add(sEntity);
            context.SaveChanges();

            return insertedRow.Entity.ToDomainEntity();
        }

        public IEnumerable<IUserRole> AddRange(IEnumerable<Domain.Models.UserRole> entities)
        {
            throw new NotImplementedException();
            //var sEntities = entities.Select(x => x.ToPersistentEntity()).AsEnumerable();
            //var insertedRows = context.UserRoles.AddRange(sEntities);
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

        public bool SoftDeleteRange(IEnumerable<Domain.Models.UserRole> entities)
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
            var sEntity = context.UserRoles.SingleOrDefault(x => x.Id == id);
            if (sEntity == null) return false;

            context.UserRoles.Remove(sEntity);
            context.SaveChanges();
            return true;
        }

        public bool DeleteRange(IEnumerable<Domain.Models.UserRole> entities)
        {
            var sEntities = entities.Select(x => x.ToPersistentEntity()).AsEnumerable();
            context.UserRoles.RemoveRange(sEntities);

            context.SaveChanges();
            return true;
        }

        public IUserRole Update(Domain.Models.UserRole entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var sEntity = context.UserRoles.Find(entity.Id);
            sEntity.Name = entity.Name;
            sEntity.LastModifiedOn = DateTime.Now;

            context.SaveChanges();
            return sEntity.ToDomainEntity();
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
