using Microsoft.AspNetCore.Authorization;
using Pointwise.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pointwise.API.Admin.Attributes
{
    public sealed class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public CustomAuthorizeAttribute(EntityType entityType, AccessType accessType)
            : base()
        {

            Roles = Enum.GetName(typeof(EntityType), entityType) + Enum.GetName(typeof(AccessType), accessType) + ",Admin";
        }

        public CustomAuthorizeAttribute() : base()
        {
            Roles = "Admin";
        }
    }
}
