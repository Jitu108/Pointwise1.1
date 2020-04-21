using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pointwise.Domain.ValidationRules
{
    public static class TagValidator
    {
        public static class Name
        {
            public const bool IsRequired = true;
            public const string IsRequiredErrorMessage = "Please provide Tag Name";
            public const int MaxLength = StringLength.C50;
            public const string MaxLenghtErrorMessage = "Max length for Name is 50";
        }
    }
}
