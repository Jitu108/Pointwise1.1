using Pointwise.Domain.Interfaces;

namespace Pointwise.Domain.Models
{
    public sealed class UserPhone : BaseEntity, IUserPhone
    {
        public int Id { get; set; }
        public IUser User { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsPrimary { get; set; }
    }
}
