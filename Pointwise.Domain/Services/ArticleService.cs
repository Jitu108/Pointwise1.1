using System;
using System.Collections.Generic;
using Pointwise.Domain.Interfaces;
using Pointwise.Domain.Repositories;
using Pointwise.Domain.ServiceInterfaces;
using Pointwise.Domain.Enums;
using Pointwise.Domain.Models;
using System.Linq;

namespace Pointwise.Domain.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository articleRepository;
        private readonly ITagService tagService;
        private readonly IImageService imageService;

        public ArticleService(
            IArticleRepository articleRepository
            , ITagService tagService
            , IImageService imageService
            )
        {
            this.articleRepository = articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
            this.tagService = tagService ?? throw new ArgumentNullException(nameof(tagService));
            this.imageService = imageService ?? throw new ArgumentNullException(nameof(imageService));
        }

        public IEnumerable<IArticle> GetAllArticles()
        {
            return articleRepository.GetAll();
        }

        public IArticle GetById(int id)
        {
            return articleRepository.GetById(id);
        }

        public IEnumerable<IArticle> GetArticlesByAuthor(string author)
        {
            return articleRepository.GetAll().Where(x => x.Author.Contains(author));
        }

        public IEnumerable<IArticle> GetArticleByTitle(string titleString)
        {
            return articleRepository.GetAll().Where(x => x.Title.Contains(titleString));
        }

        public IEnumerable<IArticle> GetArticleByDescription(string descString)
        {
            return articleRepository.GetAll().Where(x => x.Summary.Contains(descString));
        }

        public IEnumerable<IArticle> GetArticleByContent(string contentString)
        {
            return articleRepository.GetAll().Where(x => x.Content.Contains(contentString));
        }

        public IEnumerable<IArticle> GetArticleBySource(int sourceId)
        {
            return articleRepository.GetAll().Where(x => x.Source.Id == sourceId);
        }

        public IEnumerable<IArticle> GetArticleByCategory(int categoryId)
        {
            return articleRepository.GetAll().Where(x => x.Category.Id == categoryId);
        }

        public IEnumerable<IArticle> GetArticleByAssetType(ArticleAssociatedAssetType assetType)
        {
            return articleRepository.GetAll().Where(x => x.AssetType == assetType);
        }

        public IArticle Add(Article entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            entity.Tags = tagService.GetByName(entity.Tags.Select(x => x.Name)).ToList();
            
            var addedArticle = articleRepository.Add(entity);

            return addedArticle;
        }

        public bool SoftDelete(int id)
        {
            return articleRepository.SoftDelete(id);
        }

        public bool UndoSoftDelete(int id)
        {
            return articleRepository.UndoSoftDelete(id);
        }

        public bool Delete(int id)
        {
            return articleRepository.Delete(id);
        }

        public IArticle Update(Article entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            imageService.Update(entity.Image as Image, entity.Id);

            entity.Tags = tagService.GetByName(entity.Tags.Select(x => x.Name)).ToList();
            return articleRepository.Update(entity);
        }
    }
}
