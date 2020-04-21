using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pointwise.Domain.ValidationRules
{
    public static class UserRoleValidator
    {
        public static class Name
        {
            public const bool IsRequired = true;
            public const string IsRequiredErrorMessage = "Please provide UserRole Text";
            public const int MaxLength = StringLength.C50;
            public const string MaxLenghtErrorMessage = "Max length for UserRole Text is 50";
        }
    }
}
