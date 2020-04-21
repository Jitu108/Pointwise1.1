﻿namespace Pointwise.Domain.ValidationRules
{
    public static class SourceValidator
    {
        public static class Name
        {
            public const bool IsRequired = true;
            public const string IsRequiredErrorMessage = "Please provide Source Name";
            public const int MaxLength = StringLength.C2000;
            public const string MaxLenghtErrorMessage = "Max length for Name is 2000";
        }
    }
}
