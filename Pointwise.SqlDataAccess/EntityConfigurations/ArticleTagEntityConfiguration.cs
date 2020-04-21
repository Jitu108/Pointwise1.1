using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pointwise.SqlDataAccess.Models;

namespace Pointwise.SqlDataAccess.EntityConfigurations
{
    public class ArticleTagEntityConfiguration
    {
        public ArticleTagEntityConfiguration(EntityTypeBuilder<ArticleTag> builder)
        {
            builder.HasKey(x => new { x.ArticleId, x.TagId });
            builder.HasOne(x => x.Article).WithMany(x => x.ArticleTags).HasForeignKey(x => x.ArticleId);

            builder.HasOne(x => x.Tag).WithMany(x => x.ArticleTags).HasForeignKey(x => x.TagId);
        }
    }
}
