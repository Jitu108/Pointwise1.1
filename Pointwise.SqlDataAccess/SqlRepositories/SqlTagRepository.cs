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
    public sealed class SqlTagRepository : ITagRepository, IDisposable
    {
        private readonly PointwiseSqlContext context;

        public SqlTagRepository(DbContextOptions<PointwiseSqlContext> options)
        {
            context = new PointwiseSqlContext(options);
        }

        public IEnumerable<ITag> GetAll()
        {
            var tags = context.Tags.AsEnumerable().Select(x => x.ToDomainEntity()).ToList();
            return tags;
        }

        public ITag GetById(int id)
        {
            return context.Tags.Find(id).ToDomainEntity();
        }

        public ITag Add(Domain.Models.Tag entity)
        {
            var sEntity = entity.ToPersistentEntity();
            var insertedRow = context.Tags.Add(sEntity);
            context.SaveChanges();

            return insertedRow.Entity.ToDomainEntity();
        }

        public bool SoftDelete(int id)
        {
            var sEntity = context.Tags.SingleOrDefault(x => x.Id == id);
            if (sEntity == null) return false;

            sEntity.IsDeleted = true;
            context.SaveChanges();
            return true;
        }

        public bool UndoSoftDelete(int id)
        {
            var sEntity = context.Tags.SingleOrDefault(x => x.Id == id);
            if (sEntity == null) return false;

            sEntity.IsDeleted = false;
            context.SaveChanges();
            return true;
        }

        public bool SoftDeleteRange(IEnumerable<Domain.Models.Tag> entities)
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
            var sEntity = context.Tags.SingleOrDefault(x => x.Id == id);
            if (sEntity == null) return false;

            context.Tags.Remove(sEntity);
            context.SaveChanges();
            return true;
        }

        public ITag Update(Domain.Models.Tag entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var sEntity = context.Tags.Find(entity.Id);
            sEntity.Name = entity.Name;
            sEntity.LastModifiedOn = DateTime.Now;

            context.SaveChanges();
            return sEntity.ToDomainEntity();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public ITag GetByName(string name)
        {
            return context.Tags.Where(x => x.Name == name).FirstOrDefault().ToDomainEntity();
        }

        public IEnumerable<ITag> GetByName(IEnumerable<string> names)
        {
            return context.Tags.AsEnumerable().Where(x => names.Contains(x.Name)).Select(x => x.ToDomainEntity()).AsEnumerable();
        }

        public bool Exist(string name)
        {
            return context.Tags.Any(x => x.Name == name);
        }
    }
}
