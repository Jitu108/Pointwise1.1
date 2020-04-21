using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pointwise.Domain.ValidationRules;
using Pointwise.SqlDataAccess.Models;

namespace Pointwise.SqlDataAccess.EntityConfigurations
{
    public class SourceEntityConfiguration
    {
        public SourceEntityConfiguration(EntityTypeBuilder<Source> builder)
        {
            // Table Name
            builder.ToTable("Sources");

            // PK
            builder.HasKey(x => x.Id);

            // Property Configurations
            if (SourceValidator.Name.IsRequired)
            {
                builder.Property(x => x.Name)
                    .IsRequired();
            }

            builder.Property(x => x.Name)
                .HasMaxLength(SourceValidator.Name.MaxLength);


            // Common Property Configurations
            builder.Property(x => x.IsDeleted)
                .IsRequired()
                .HasAnnotation("Default", 0);

            builder.Property(x => x.CreatedOn)
                .IsRequired();
        }
    }
}
