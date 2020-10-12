using Pointwise.Domain.Enums;
using Pointwise.Domain.Interfaces;
using Pointwise.Domain.Models;
using Pointwise.SqlDataAccess.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pointwise.SqlDataAccess.Models
{
    public partial class SqlUserRole : BaseEntity, IUserRole
    {
        public int Id { get; set; }
        
        [NotMappedAttribute]
        public IUser User { get; set; }

        public int? UserId { get; set; }

        public virtual User SqlUser { get; set; }
        public EntityType EntityType { get; set; }
        public AccessType AccessType { get; set; }
    }
    public partial class SqlUserRole : IConvert<Domain.Models.UserRole>
    {
        public Domain.Models.UserRole ToDomainEntity()
        {
            return new Domain.Models.UserRole
            {
                Id = this.Id,
                //User = this.SqlUser.ToDomainEntity(),
                EntityType = this.EntityType,
                AccessType = this.AccessType,
                CreatedOn = this.CreatedOn,
                LastModifiedOn = this.LastModifiedOn,
                IsDeleted = this.IsDeleted
            };
        }
    }
}
