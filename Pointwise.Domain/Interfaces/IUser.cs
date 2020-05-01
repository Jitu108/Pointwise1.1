using System;
using System.Collections.Generic;
using Pointwise.Domain.Enums;

namespace Pointwise.Domain.Interfaces
{
    public interface IUser
    {
        int Id { get; set; }
        string FirstName { get; set; }
        string MiddleName { get; set; }
        string LastName { get; set; }
        string EmailAddress { get; set; }
        string PhoneNumber { get; set; }
        IUserType UserType { get; set; }
        UserNameType UserNameType { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        bool IsBlocked { get; set; }
        string Token { get; set; }
        DateTime ExpiryDate { get; set; }
        IEnumerable<IUserRole> Roles { get; set; }
        int? CreatedBy { get; set; }
        bool IsDeleted { get; set; }
        DateTime CreatedOn { get; set; }
        DateTime? LastModifiedOn { get; set; }
    }
}
