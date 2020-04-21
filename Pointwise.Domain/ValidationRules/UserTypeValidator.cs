using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pointwise.Domain.ValidationRules
{
    public static class UserTypeValidator
    {
        public static class Name
        {
            public const bool IsRequired = true;
            public const string IsRequiredErrorMessage = "Please provide UserType Text";
            public const int MaxLength = StringLength.C50;
            public const string MaxLenghtErrorMessage = "Max length for UserType Text is 50";
        }
    }
}
