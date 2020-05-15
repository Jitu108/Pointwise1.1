using System;

namespace Pointwise.API.Admin.DTO
{
    public class AuthUserDto : UserDto
    {
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
