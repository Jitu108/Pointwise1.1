using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pointwise.SqlDataAccess.Models;

namespace Pointwise.SqlDataAccess.EntityConfigurations
{
    public class ArticleEntityCofiguration
    {
        public ArticleEntityCofiguration(EntityTypeBuilder<Article> builder)
        {
            // Table Name
            builder.ToTable("Articles");

            // PK
            builder.HasKey(x => x.Id);

            // Property Configurations
            builder.Property(x => x.Author)
                .HasMaxLength(1000);

            builder.Property(x => x.Summary)
                .HasMaxLength(4000);

            // Common Property Configurations
            builder.Property(x => x.IsDeleted)
                .IsRequired()
                .HasAnnotation("Default", 0);

            builder.Property(x => x.CreatedOn)
                .IsRequired();
        }
    }
}
