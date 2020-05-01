using Pointwise.Domain.Enums;
using Pointwise.Domain.Interfaces;

namespace Pointwise.Domain.Models
{
    public class UserRole : BaseEntity, IUserRole
    {
        public int Id { get; set; }
        public IUser User { get; set; }
        public EntityType EntityType { get; set; }
        public AccessType AccessType { get; set; }
    }
}
