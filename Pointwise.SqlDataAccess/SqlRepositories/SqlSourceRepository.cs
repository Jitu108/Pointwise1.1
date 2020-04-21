using System;
using System.Linq;
using System.Collections.Generic;

using Pointwise.Domain.Interfaces;
using DomainModel = Pointwise.Domain.Models;
using Pointwise.Domain.Repositories;
using Pointwise.SqlDataAccess.ModelExtensions;
using System.Diagnostics;
using Pointwise.SqlDataAccess.SQLContext;
using Microsoft.EntityFrameworkCore;
using Pointwise.SqlDataAccess.Models;

namespace Pointwise.SqlDataAccess.SqlRepositories
{
    public sealed class SqlSourceRepository : ISourceRepository, IDisposable
    {
        private readonly PointwiseSqlContext context;

        public SqlSourceRepository(DbContextOptions<PointwiseSqlContext> options)
        {
            context = new PointwiseSqlContext(options);

        }

        public IEnumerable<ISource> GetAll()
        {
            return context.Sources.AsEnumerable().Select(x => x.ToDomainEntity());
        }

        public ISource GetById(int id)
        {
            return context.Sources.Find(id).ToDomainEntity();
        }

        public ISource Add(DomainModel.Source entity)
        {
                var sEntity = entity.ToPersistentEntity();
                var insertedRow = context.Sources.Add(sEntity);
                context.SaveChanges();

                return insertedRow.Entity.ToDomainEntity();
        }

        public IEnumerable<ISource> AddRange(IEnumerable<DomainModel.Source> entities)
        {
            throw new NotImplementedException();
            //var sEntities = entities.Select(x => x.ToPersistentEntity()).AsEnumerable();
            //var insertedRows = context.Sources.AddRange(sEntities);
            //context.SaveChanges();

            //return insertedRows.Select(x => x.ToDomainEntity()).AsEnumerable();
        }

        public bool SoftDelete(int id)
        {
            var sEntity = context.Sources.SingleOrDefault(x => x.Id == id);
            if (sEntity == null) return false;
            sEntity.IsDeleted = true;
            context.SaveChanges();
            return true;
        }

        public bool UndoSoftDelete(int id)
        {
            var sEntity = context.Sources.SingleOrDefault(x => x.Id == id);
            if (sEntity == null) return false;
            sEntity.IsDeleted = false;
            context.SaveChanges();
            return true;
        }

        public bool SoftDeleteRange(IEnumerable<DomainModel.Source> entities)
        {
            var sEntities = entities.Select(x => x.ToPersistentEntity()).AsEnumerable();
            foreach(var source in sEntities)
            {
                source.IsDeleted = true;
            }
            context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var sEntity = context.Sources.SingleOrDefault(x => x.Id == id);
            if (sEntity == null) return false;

            context.Sources.Remove(sEntity);
            context.SaveChanges();
            return true;
        }

        public bool DeleteRange(IEnumerable<DomainModel.Source> entities)
        {
            var sEntities = entities.Select(x => x.ToPersistentEntity()).AsEnumerable();
            context.Sources.RemoveRange(sEntities);
            context.SaveChanges();
            return true;
        }

        public ISource Update(DomainModel.Source entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var sEntity = context.Sources.Find(entity.Id);
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
            return context.Sources.Any(x => x.Name == name);
        }
    }
}
