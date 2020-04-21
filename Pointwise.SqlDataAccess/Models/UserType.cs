using Pointwise.Domain.Interfaces;
using Pointwise.Domain.Models;
using Pointwise.SqlDataAccess.Interfaces;
using System.Collections.Generic;

namespace Pointwise.SqlDataAccess.Models
{
    public partial class SqlUserType : BaseEntity, IUserType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
    public partial class SqlUserType : IConvert<Domain.Models.UserType>
    {
        public Domain.Models.UserType ToDomainEntity()
        {
            return new Domain.Models.UserType
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
