using Pointwise.Domain.Enums;
using Pointwise.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Pointwise.Domain.Models
{
    public sealed class Article : BaseEntity, IArticle
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Url { get; set; }
        public DateTime? PublicationDate { get; set; }
        public string Content { get; set; }
        public string Synopsis { get; set; }
        public ISource Source { get; set; }
        public ICategory Category { get; set; }
        public ArticleAssociatedAssetType AssetType { get; set; }
        public IList<ITag> Tags { get; set; }
        public IImage Image { get; set; }

    }
}
