using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pointwise.Domain.ValidationRules;
using Pointwise.SqlDataAccess.Models;

namespace Pointwise.SqlDataAccess.EntityConfigurations
{
    public class UserEntityConfiguration
    {
        public UserEntityConfiguration(EntityTypeBuilder<User> builder)
        {
            // Table Name
            builder.ToTable("Users");

            // PK
            builder.HasKey(x => x.Id);

            // Property Configurations
            if (UserValidator.FirstName.IsRequired)
            {
                builder.Property(x => x.FirstName).IsRequired();
            }
            builder.Property(x => x.FirstName)
                .HasMaxLength(UserValidator.FirstName.MaxLength);

            //---------------------------------------------------
            if (UserValidator.MiddleName.IsRequired)
            {
                builder.Property(x => x.MiddleName).IsRequired();
            }
            builder.Property(x => x.MiddleName)
                .HasMaxLength(UserValidator.MiddleName.MaxLength);

            //---------------------------------------------------
            if (UserValidator.LastName.IsRequired)
            {
                builder.Property(x => x.LastName).IsRequired();
            }
            builder.Property(x => x.LastName)
                .HasMaxLength(UserValidator.LastName.MaxLength);

            //---------------------------------------------------
            if (UserValidator.EmailAddress.IsRequired)
            {
                builder.Property(x => x.EmailAddress).IsRequired();
            }
            builder.Property(x => x.EmailAddress)
                .HasMaxLength(UserValidator.EmailAddress.MaxLength);

            //---------------------------------------------------
            if (UserValidator.PhoneNumber.IsRequired)
            {
                builder.Property(x => x.PhoneNumber).IsRequired();
            }
            builder.Property(x => x.PhoneNumber)
                    .HasMaxLength(UserValidator.PhoneNumber.MaxLength);

            //---------------------------------------------------
            if (UserValidator.UserType.IsRequired)
            {
                builder.Property(x => x.UserTypeId).IsRequired();
            }

            builder.HasOne(x => x.SqlUserType)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.UserTypeId);

            // builder.HasMany(u => u.SqlUserRoles)
            //    .WithMany(r => r.Users)
            //    .Map(m =>
            //    {
            //        m.MapLeftKey("UserId");
            //        m.MapRightKey("RoleId");
            //        m.ToTable("UserRoles");
            //    });

            //---------------------------------------------------
            if (UserValidator.UserNameType.IsRequired)
            {
                builder.Property(x => x.UserNameType).IsRequired();
            }

            //---------------------------------------------------
            if (UserValidator.UserName.IsRequired)
            {
                builder.Property(x => x.UserName).IsRequired();
            }
            builder.Property(x => x.UserName)
                .HasMaxLength(UserValidator.UserName.MaxLength);

            //---------------------------------------------------
            if (UserValidator.Password.IsRequired)
            {
                builder.Property(x => x.Password).IsRequired();
            }
            builder.Property(x => x.Password)
                .HasMaxLength(UserValidator.Password.MaxLength);

            //---------------------------------------------------
            if (UserValidator.IsBlocked.IsRequired)
            {
                builder.Property(x => x.IsBlocked).IsRequired();
            }


            builder.Property(x => x.CreatedOn)
                .IsRequired();
        }
    }
}
