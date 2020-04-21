using System;
using System.Linq;
using System.Collections.Generic;
using Pointwise.Domain.Interfaces;
using Pointwise.Domain.Repositories;
using Pointwise.SqlDataAccess.ModelExtensions;
using Pointwise.SqlDataAccess.Models;
using Pointwise.SqlDataAccess.SQLContext;
using Microsoft.EntityFrameworkCore;

namespace Pointwise.SqlDataAccess.SqlRepositories
{
    public sealed class SqlCategoryRepository : ICategoryRepository, IDisposable
    {
        private readonly PointwiseSqlContext context;

        public SqlCategoryRepository(DbContextOptions<PointwiseSqlContext> options)
        {
            context = new PointwiseSqlContext(options);

        }


        public IEnumerable<ICategory> GetAll()
        {
            var categories = context.Categories.AsEnumerable().Select(x => x.ToDomainEntity()).ToList();
            return categories;
        }

        public ICategory GetById(int id)
        {
            return context.Categories.Find(id).ToDomainEntity();
        }

        public ICategory Add(Domain.Models.Category entity)
        {
            var sEntity = entity.ToPersistentEntity();
            var insertedRow = context.Categories.Add(sEntity);
            context.SaveChanges();

            return insertedRow.Entity.ToDomainEntity();
        }

        public bool SoftDelete(int id)
        {
            var sEntity = context.Categories.SingleOrDefault(x => x.Id == id);
            if (sEntity == null) return false;
            sEntity.IsDeleted = true;
            context.SaveChanges();
            return true;
        }

        public bool UndoSoftDelete(int id)
        {
            var sEntity = context.Categories.SingleOrDefault(x => x.Id == id);
            if (sEntity == null) return false;
            sEntity.IsDeleted = false;
            context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var sEntity = context.Categories.SingleOrDefault(x => x.Id == id);

            if (sEntity == null) return false;

            context.Categories.Remove(sEntity);
            context.SaveChanges();
            return true;
        }

        public ICategory Update(Domain.Models.Category entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var sEntity = context.Categories.Find(entity.Id);
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
            return context.Categories.Any(x => x.Name == name);
        }
    }
}
