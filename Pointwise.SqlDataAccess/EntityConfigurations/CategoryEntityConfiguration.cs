using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pointwise.Domain.ValidationRules;
using Pointwise.SqlDataAccess.Models;
using System;

namespace Pointwise.SqlDataAccess.EntityConfigurations
{
    class CategoryEntityConfiguration
    {
        public CategoryEntityConfiguration(EntityTypeBuilder<Category> builder)
        {
            // Table Name
            builder.ToTable("Categories");

            // PK
            builder.HasKey(x => x.Id);

            // Property Configurations

            if (CategoryValidator.Name.IsRequired)
            {
                builder.Property(x => x.Name).IsRequired();
            }

            builder.Property(x => x.Name)
                .HasMaxLength(CategoryValidator.Name.MaxLength);


            // Common Property Configurations
            builder.Property(x => x.IsDeleted)
                .IsRequired()
                .HasAnnotation("Default", 0);

            builder.Property(x => x.CreatedOn)
                .IsRequired();
        }
    }
}
