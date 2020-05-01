using Pointwise.Domain.Interfaces;

namespace Pointwise.Domain.Models
{
    public class UserType : IUserType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
