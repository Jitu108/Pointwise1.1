using Pointwise.Domain.Interfaces;
using Pointwise.Domain.Models;
using Pointwise.SqlDataAccess.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pointwise.SqlDataAccess.Models
{
    public partial class Source : BaseEntity, ISource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Article> SqlArticles { get; set; }

        [NotMappedAttribute]
        public IList<IArticle> Articles
        {
            get { return (IList<IArticle>)SqlArticles; }
            set { SqlArticles = (IList<Article>)value; }
        }
    }

    public partial class Source : IConvert<Domain.Models.Source>
    {
        public Domain.Models.Source ToDomainEntity()
        {
            return new Domain.Models.Source
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
