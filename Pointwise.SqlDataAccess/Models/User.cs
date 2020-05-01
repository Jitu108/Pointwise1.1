using System;
using System.Linq;
using System.Collections.Generic;
using Pointwise.Domain.Enums;
using Pointwise.Domain.Interfaces;
using Pointwise.Domain.Models;
using Pointwise.SqlDataAccess.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pointwise.SqlDataAccess.Models
{
    public partial class User : IUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }

        public SqlUserType SqlUserType { get; set; }

        [NotMappedAttribute]
        public IUserType UserType
        {
            get { return SqlUserType as IUserType; }
            set { SqlUserType = value as SqlUserType; }
        }

        public int? UserTypeId { get; set; }

        public IList<SqlUserRole> SqlUserRoles { get; set; }

        [NotMappedAttribute]
        public virtual IEnumerable<IUserRole> Roles
        {
            get { return SqlUserRoles != null? SqlUserRoles.Cast<IUserRole>().ToList() : new List<IUserRole>(); }
            set { SqlUserRoles = value.Select(x => x as SqlUserRole).ToList(); }
        }

        public UserNameType UserNameType { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsBlocked { get; set; }
        [NotMapped]
        public string Token { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        [NotMapped]
        public DateTime ExpiryDate { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public int? CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
    }


    public partial class User : IConvert<Domain.Models.User>
    {
        public Domain.Models.User ToDomainEntity()
        {
            var user = new Domain.Models.User
            {
                Id = this.Id,
                FirstName = this.FirstName,
                MiddleName = this.MiddleName,
                LastName = this.LastName,
                EmailAddress = this.EmailAddress,
                PhoneNumber = this.PhoneNumber,
                UserType = this.UserType,
                UserNameType = this.UserNameType,
                UserName = this.UserName,
                Password = this.Password,
                IsBlocked = this.IsBlocked,
                CreatedOn = this.CreatedOn,
                LastModifiedOn = this.LastModifiedOn,
                IsDeleted = this.IsDeleted
            };

            user.Roles =
                this.SqlUserRoles != null
                ? this.SqlUserRoles.Select(x => x.ToDomainEntity()) : new List<UserRole>();

            return user;
        }
    }
}
