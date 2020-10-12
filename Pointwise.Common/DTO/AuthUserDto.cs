using System;
using System.Collections.Generic;
using System.Text;

namespace Pointwise.Common.DTO
{
    public class AuthUserDto : UserDto
    {
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
