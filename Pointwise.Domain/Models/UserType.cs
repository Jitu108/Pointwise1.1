using Pointwise.Domain.Interfaces;

namespace Pointwise.Domain.Models
{
    public class UserType : BaseEntity, IUserType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
