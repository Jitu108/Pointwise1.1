using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pointwise.Domain.ValidationRules;
using Pointwise.SqlDataAccess.Models;

namespace Pointwise.SqlDataAccess.EntityConfigurations
{
    public class TagEntityConfiguration
    {
        public TagEntityConfiguration(EntityTypeBuilder<Tag> builder)
        {
            // Table Name
            builder.ToTable("Tags");

            // PK
            builder.HasKey(x => x.Id);

            // Property Configurations

            if (TagValidator.Name.IsRequired)
            {
                builder.Property(x => x.Name).IsRequired();
            }

            builder.Property(x => x.Name)
                .HasMaxLength(TagValidator.Name.MaxLength);


            // Common Property Configurations
            builder.Property(x => x.IsDeleted)
                .IsRequired()
                .HasAnnotation("Default", 0);

            builder.Property(x => x.CreatedOn)
                .IsRequired();
        }
    }
}
