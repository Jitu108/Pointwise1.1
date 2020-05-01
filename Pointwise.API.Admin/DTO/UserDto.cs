using Pointwise.Domain.Enums;
using Pointwise.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pointwise.API.Admin.DTO
{
    public class UserDto
    {
         public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string UserType { get; set; }
        public string UserNameType { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsBlocked { get; set; }
        public string Token { get; set; }
        public IList<Role> Roles { get; set; }
    }

    public class Role
    {
        public string EntityType { get; set; }
        public string AccessType { get; set; }
    }
}
