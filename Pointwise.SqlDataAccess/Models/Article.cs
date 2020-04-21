using Pointwise.Domain.Enums;
using Pointwise.Domain.Interfaces;
using Pointwise.SqlDataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Pointwise.SqlDataAccess.Models
{
    public partial class Article : Domain.Models.BaseEntity, IArticle
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Url { get; set; }
        public DateTime? PublicationDate { get; set; }
        public string Content { get; set; }
        public string Synopsis { get; set; }

        public virtual Source SqlSource { get; set; }

        [NotMappedAttribute]
        public virtual ISource Source { 
            get { return SqlSource as ISource; } 
            set { SqlSource = value as Source; } 
        }
        public int? SourceId { get; set; }

        public virtual Category SqlCategory { get; set; }
        
        [NotMappedAttribute]
        public virtual ICategory Category {
            get { return SqlCategory as ICategory; }
            set { SqlCategory = value as Category; }
        }
        public int? CategoryId { get; set; }
        public virtual ArticleAssociatedAssetType AssetType { get; set; }

        public virtual IList<ArticleTag> ArticleTags { get; set; }

        [NotMappedAttribute]
        public virtual IList<ITag> Tags { get; set; }

        public virtual Image SqlImage { get; set; }

        [NotMappedAttribute]
        public virtual IImage Image { get; set; }
    }

    public partial class Article : IConvert<Domain.Models.Article>
    {
        public Domain.Models.Article ToDomainEntity()
        {
            //var tags = this.ArticleTags.Select(x => new Tag { Id = x.Tag.Id, Name = x.Tag.Name }).ToList();
            var article =  new Domain.Models.Article
            {
                Id = this.Id,
                Author = this.Author,
                Title = this.Title,
                Summary = this.Summary,
                Url = this.Url,
                PublicationDate = this.PublicationDate,
                Content = this.Content,
                Synopsis = this.Synopsis,
                Source = this.SqlSource != null? this.SqlSource.ToDomainEntity() as ISource : new Source(),
                Category = this.SqlCategory != null ? this.SqlCategory.ToDomainEntity() as ICategory : new Category(),
                AssetType = this.AssetType,
                CreatedOn = this.CreatedOn,
                LastModifiedOn = this.LastModifiedOn,
                IsDeleted = this.IsDeleted
            };
            article.Tags = 
                this.ArticleTags != null 
                ? this.ArticleTags.Select(x => x.Tag != null ? x.Tag.ToDomainEntity() : new Domain.Models.Tag()).Cast<ITag>().ToList() 
                : new List<ITag>();

            article.Image = this.SqlImage != null ? this.SqlImage.ToDomainEntity() as IImage : new Image();
            return article;
        }
    }
}
