using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pointwise.Domain.ValidationRules
{
    public static class UserValidator
    {
        public static class FirstName
        {
            public const bool IsRequired = true;
            public const string IsRequiredErrorMessage = "Please provide UserType Text";
            public const int MaxLength = StringLength.C100;
            public const string MaxLenghtErrorMessage = "Max length for UserType Text is 100";
        }

        public static class MiddleName
        {
            public const bool IsRequired = false;
            public const int MaxLength = StringLength.C100;
            public const string MaxLenghtErrorMessage = "Max length for Middle Name is 100";
        }

        public static class LastName
        {
            public const bool IsRequired = false;
            public const int MaxLength = StringLength.C100;
            public const string MaxLenghtErrorMessage = "Max length for Second Name is 100";
        }

        public static class EmailAddress
        {
            public const bool IsRequired = false;
            public const int MaxLength = StringLength.C256;
            public const string MaxLenghtErrorMessage = "Max length for Email is 256";
            public const string PatternErrorMessage = "Email address is not in correct format";
        }

        public static class PhoneNumber {
            public const bool IsRequired = false;
            public const int MaxLength = StringLength.C50;
            public const string MaxLenghtErrorMessage = "Max length for Email is 50";
            public const string AlphabetCharErrorMessage = "Phone number cannot contain alphabets";
        }

        public static class UserType
        {
            public const bool IsRequired = true;
            public const string IsRequiredErrorMessage = "Please provide UserType";
        }

        public static class UserNameType
        {
            public const bool IsRequired = true;
            public const string IsRequiredErrorMessage = "Please provide UserNameType";
        }

        public static class UserName
        {
            public const bool IsRequired = true;
            public const string IsRequiredErrorMessage = "Please provide User Name";
            public const int MaxLength = StringLength.C50;
            public const string MaxLenghtErrorMessage = "Max length for User name is 50";
            public const string UserNameNotUniqueErrorMessage = "The User name has already taken. Please choose another User name";
        }

        public static class Password
        {
            public const bool IsRequired = true;
            public const string IsRequiredErrorMessage = "Please provide Password";
            public const int MaxLength = StringLength.C50;
            public const string MaxLenghtErrorMessage = "Max length for Password is 50";
        }

        public static class IsBlocked
        {
            public const bool IsRequired = false;
        }
    }
}
