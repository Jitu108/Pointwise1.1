using System;
using System.Collections.Generic;
using System.Text;

namespace Pointwise.Domain.ValidationRules
{
    public static class ImageValidator
    {
        public static class Caption
        {
            public const bool IsRequired = true;
            public const string IsRequiredErrorMessage = "Please provide Tag Name";
            public const int MaxLength = StringLength.C1000;
            public const string MaxLenghtErrorMessage = "Max length for Caption is 1000";
        }
    }
}
