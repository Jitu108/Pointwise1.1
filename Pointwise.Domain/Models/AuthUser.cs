using Pointwise.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pointwise.Domain.Models
{
    public class AuthUser : User, IAuthUser
    {
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
