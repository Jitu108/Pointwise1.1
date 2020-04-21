using Pointwise.Domain.Interfaces;

namespace Pointwise.Domain.Models
{
    public sealed class UserEmail : BaseEntity, IUserEmail
    {
        public int Id { get; set; }
        public IUser User { get; set; }
        public string EmailAddress { get; set; }
        public bool IsPrimary { get; set; }
    }
}
