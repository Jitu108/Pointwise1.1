using Pointwise.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Pointwise.Domain.Interfaces
{
    public interface IArticle : IBaseEntity
    {
        int Id { get; set; }
        string Author { get; set; }
        string Title { get; set; }
        string Summary { get; set; }
        string Url { get; set; }
        DateTime? PublicationDate { get; set; }
        string Content { get; set; }
        string Synopsis { get; set; }
        ISource Source { get; set; }
        ICategory Category { get; set; }
        ArticleAssociatedAssetType AssetType { get; set; }
        IList<ITag> Tags { get; set; }
        IImage Image { get; set; }
    }
}
