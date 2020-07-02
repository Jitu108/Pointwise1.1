using System;
using System.Linq;
using System.Collections.Generic;
using Pointwise.Domain.Interfaces;
using Pointwise.Domain.Repositories;
using Pointwise.SqlDataAccess.ModelExtensions;
using Pointwise.SqlDataAccess.SQLContext;
using Microsoft.EntityFrameworkCore;
using Pointwise.SqlDataAccess.Models;

namespace Pointwise.SqlDataAccess.SqlRepositories
{
    public sealed class SqlArticleRepository : IArticleRepository, IDisposable
    {
        private readonly PointwiseSqlContext context;

        public SqlArticleRepository(DbContextOptions<PointwiseSqlContext> options)
        {
            context = new PointwiseSqlContext(options);
        }

        public IEnumerable<IArticle> GetAll()
        {
            var articles = context.Articles.AsNoTracking()
                .Include(x => x.SqlSource).AsNoTracking()
                .Include(x => x.SqlCategory).AsNoTracking()
                .Include(x => x.ArticleTags).ThenInclude(x => x.Tag).AsNoTracking()
                .Include(x => x.SqlImage).AsNoTracking()
                .ToList();
            var domainArticles = articles.Select(x => x.ToDomainEntity()).ToList();
            return domainArticles;
        }
        public IArticle GetById(int id)
        {
            try
            {
                var article = context.Articles.AsNoTracking()
                    .Include(x => x.SqlSource).AsNoTracking()
                    .Include(x => x.SqlCategory).AsNoTracking()
                    .Include(x => x.ArticleTags).ThenInclude(x => x.Tag).AsNoTracking()
                    .Include(x => x.SqlImage).AsNoTracking()
                    .Where(x => x.Id == id).FirstOrDefault();
                return article.ToDomainEntity();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IArticle Add(Domain.Models.Article entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            //context.Sources.Attach((Source)entity.Source);

            var tagIds = entity.Tags != null
                ? entity.Tags.Select(x => x.Id) 
                : new List<int>();

            var sEntity = entity.ToPersistentEntity();

            sEntity.Category = entity.Category != null 
                ? context.Categories.Where(x => x.Id == entity.Category.Id).FirstOrDefault()
                : null;

            sEntity.Source = entity.Source != null 
                ? context.Sources.Where(x => x.Id == entity.Source.Id).FirstOrDefault()
                : null;

            //sEntity.ArticleTags =  context.Tags.Where(x => tagIds.Contains(x.Id)).Select(x => x).ToList();

            var insertedRow = context.Articles.Add(sEntity);
            context.SaveChanges();

            return insertedRow.Entity.ToDomainEntity();
        }

        public bool SoftDelete(int id)
        {
            var sEntity = context.Articles.FirstOrDefault(x => x.Id == id);
            if (sEntity == null) return false;
            sEntity.IsDeleted = true;
            context.SaveChanges();
            return true;
        }

        public bool UndoSoftDelete(int id)
        {
            var sEntity = context.Articles.FirstOrDefault(x => x.Id == id);
            if (sEntity == null) return false;
            sEntity.IsDeleted = false;
            context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var sEntity = context.Articles.SingleOrDefault(x => x.Id == id);
            if (sEntity == null) return false;
            context.Articles.Remove(sEntity);
            context.SaveChanges();
            return true;
        }

        public IArticle Update(Domain.Models.Article entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var sEntity = context.Articles
                .Include(x => x.ArticleTags)
                .Where(x => x.Id == entity.Id)
                .FirstOrDefault();

            if (sEntity == null) return null;

            var tagsToUpdate = entity.Tags
                .Select(x => new ArticleTag { ArticleId = entity.Id, TagId = x.Id})
                .ToList();

            if (entity.Source != null) sEntity.SourceId = entity.Source.Id;
            if (entity.Category != null) sEntity.CategoryId = entity.Category.Id;

            sEntity.Title = entity.Title;
            sEntity.SubTitle = entity.SubTitle;
            sEntity.Url = entity.Url;
            sEntity.PublicationDate = entity.PublicationDate;
            sEntity.Summary = entity.Summary;
            sEntity.ArticleTags = tagsToUpdate;

            sEntity.LastModifiedOn = DateTime.Now;

            context.SaveChanges();
            return GetById(sEntity.Id);
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
