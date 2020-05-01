using System.Collections.Generic;
using Pointwise.Domain.Interfaces;
using Pointwise.Domain.Enums;
using System;

namespace Pointwise.Domain.ServiceInterfaces
{
    public interface IArticleService
    {
        IEnumerable<IArticle> GetAllArticles();
        IArticle GetById(int id);
        IEnumerable<IArticle> GetArticleByTitle(string titleString);
        IEnumerable<IArticle> GetArticleByDescription(string descString);
        IEnumerable<IArticle> GetArticleBySource(int sourceId);
        IEnumerable<IArticle> GetArticleByCategory(int categoryId);
        IEnumerable<IArticle> GetArticleByAssetType(ArticleAssociatedAssetType AssetType);

        IArticle Add(Models.Article entity);

        bool Delete(int id);

        bool SoftDelete(int id);
        bool UndoSoftDelete(int id);

        IArticle Update(Models.Article entity);
    }
}
