using Pointwise.Domain.Interfaces;

namespace Pointwise.Domain.Models
{
    public class UserRole : BaseEntity, IUserRole
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
