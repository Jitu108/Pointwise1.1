using Pointwise.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pointwise.API.Admin.Roles
{
    public class CustomRoles
    {
        public const string Admin = "Admin";
        public const string Author = "Author";
        public const string AdminOrAuthor = Admin + "," + Author;
    }
}
