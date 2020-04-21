using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Pointwise.Domain.Interfaces;

namespace Pointwise.SqlDataAccess.Models
{
    public partial class ArticleTag
    {
        [Key]
        public int ArticleId { get; set; }
        public Article Article { get; set; }

        [Key]
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }

    public partial class ArticleTag
    {
        public Domain.Models.Tag ToTagDomainEntity()
        {
            return new Domain.Models.Tag
            {
                Id = this.Tag.Id,
                Name = this.Tag.Name,
                CreatedOn = this.Tag.CreatedOn,
                LastModifiedOn = this.Tag.LastModifiedOn,
                IsDeleted = this.Tag.IsDeleted
            };
        }

        public Domain.Models.Article ToArticleDomainEntity()
        {
            return new Domain.Models.Article
            {
                Id = this.Article.Id,
                Author = this.Article.Author,
                Title = this.Article.Title,
                Summary = this.Article.Summary,
                Url = this.Article.Url,
                PublicationDate = this.Article.PublicationDate,
                Content = this.Article.Content,
                Synopsis = this.Article.Synopsis,
                Source = this.Article.SqlSource != null ? this.Article.SqlSource as ISource : new Source(),
                Category = this.Article.SqlCategory != null ? this.Article.SqlCategory as ICategory : new Category(),
                AssetType = this.Article.AssetType,
                //Tags = this.Article.ArticleTags != null ? this.Article.ArticleTags.Select(x => x.Tag).Cast<ITag>().ToList() : new List<ITag>(),
                CreatedOn = this.Article.CreatedOn,
                LastModifiedOn = this.Article.LastModifiedOn,
                IsDeleted = this.Article.IsDeleted
            };
        }
    }
}
