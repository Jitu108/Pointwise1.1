using Pointwise.Domain.Enums;
using Pointwise.Domain.Interfaces;

namespace Pointwise.Domain.Models
{
    public sealed class User : BaseEntity, IUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public IUserType UserType { get; set; }
        public UserNameType UserNameType { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsBlocked { get; set; }
        public string Token { get; set; }

    }
}
