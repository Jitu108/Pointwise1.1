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
    public sealed class SqlUserTypeRepository : IUserTypeRepository, IDisposable
    {
        private readonly PointwiseSqlContext context;

        public SqlUserTypeRepository(DbContextOptions<PointwiseSqlContext> options)
        {
            context = new PointwiseSqlContext(options);
        }

        public IEnumerable<IUserType> GetAll()
        {
            var userTypes = context.UserTypes.AsEnumerable().Select(x => x.ToDomainEntity()).ToList();
            return userTypes;
        }

        public IUserType GetById(int id)
        {
            return context.UserTypes.Find(id).ToDomainEntity();
        }

        public IUserType Add(Domain.Models.UserType entity)
        {
            var sEntity = entity.ToPersistentEntity();
            var insertedRow = context.UserTypes.Add(sEntity);
            context.SaveChanges();

            return insertedRow.Entity.ToDomainEntity();
        }

        public IEnumerable<IUserType> AddRange(IEnumerable<Domain.Models.UserType> entities)
        {
            throw new NotImplementedException();
            //var sEntities = entities.Select(x => x.ToPersistentEntity()).AsEnumerable();
            //var insertedRows = context.UserTypes.AddRange(sEntities);
            //context.SaveChanges();

            //return insertedRows.Select(x => x.ToDomainEntity()).AsEnumerable();
        }

        public bool SoftDelete(int id)
        {
            var sEntity = context.UserTypes.SingleOrDefault(x => x.Id == id);
            if (sEntity == null) return false;

            sEntity.IsDeleted = true;
            context.SaveChanges();
            return true;
        }

        public bool UndoSoftDelete(int id)
        {
            var sEntity = context.UserTypes.SingleOrDefault(x => x.Id == id);
            if (sEntity == null) return false;

            sEntity.IsDeleted = false;
            context.SaveChanges();
            return true;
        }

        public bool SoftDeleteRange(IEnumerable<Domain.Models.UserType> entities)
        {
            var sEntities = entities.Select(x => x.ToPersistentEntity()).AsEnumerable();
            //context.Categories.RemoveRange(sEntities);
            foreach (var entity in sEntities)
            {
                entity.IsDeleted = true;
            }
            context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var sEntity = context.UserTypes.SingleOrDefault(x => x.Id == id);
            if (sEntity == null) return false;

            context.UserTypes.Remove(sEntity);
            context.SaveChanges();
            return true;
        }

        public bool DeleteRange(IEnumerable<Domain.Models.UserType> entities)
        {
            var sEntities = entities.Select(x => x.ToPersistentEntity()).AsEnumerable();
            context.UserTypes.RemoveRange(sEntities);

            context.SaveChanges();
            return true;
        }

        public IUserType Update(Domain.Models.UserType entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var sEntity = context.UserTypes.Find(entity.Id);
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