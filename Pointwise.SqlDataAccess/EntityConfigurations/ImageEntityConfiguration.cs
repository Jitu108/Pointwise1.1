using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pointwise.Domain.ValidationRules;
using Pointwise.SqlDataAccess.Models;

namespace Pointwise.SqlDataAccess.EntityConfigurations
{
    class ImageEntityConfiguration
    {
        public ImageEntityConfiguration(EntityTypeBuilder<Image> builder)
        {
            // Table Name
            builder.ToTable("Images");

            // PK
            builder.HasKey(x => x.Id);

            // Property Configurations

            builder.Property(x => x.Caption)
                .HasMaxLength(ImageValidator.Caption.MaxLength);


            // Common Property Configurations
            builder.Property(x => x.IsDeleted)
                .IsRequired()
                .HasAnnotation("Default", 0);

            builder.Property(x => x.CreatedOn)
                .IsRequired();
        }
    }
}
