using Pointwise.Domain.Interfaces;
using Pointwise.Domain.Models;
using Pointwise.SqlDataAccess.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pointwise.SqlDataAccess.Models
{
    public partial class Category : BaseEntity, ICategory
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

    public partial class Category : IConvert<Domain.Models.Category>
    {
        public Domain.Models.Category ToDomainEntity()
        {
            return new Domain.Models.Category
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
