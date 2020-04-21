using Pointwise.Domain.Interfaces;
using Pointwise.Domain.Models;
using Pointwise.SqlDataAccess.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pointwise.SqlDataAccess.Models
{
    public partial class Tag : BaseEntity, ITag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IList<ArticleTag> ArticleTags { get; set; }

        [NotMappedAttribute]
        public IList<IArticle> Articles { get; }
    }

    public partial class Tag : IConvert<Domain.Models.Tag>
    {
        public Domain.Models.Tag ToDomainEntity()
        {
            return new Domain.Models.Tag
            {
                Id = this.Id,
                Name = this.Name,
                CreatedOn = this.CreatedOn,
                LastModifiedOn = this.LastModifiedOn,
                IsDeleted = this.IsDeleted
            };
        }
    }
}
