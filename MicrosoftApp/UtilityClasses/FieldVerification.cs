using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MicrosoftApp.UtilityClasses
{
    public static class FieldVerification
    {

        public static class Tips
        {
            public const string EmailTip = "Email must end in @gmail.com / @yahoo.com and not be empty.";
            public const string UsernameTip = "Username must contain at least 1 capital letter, 1 digit, and have a length of 6-15.";
            public const string PasswordTip = "Password must contain at least 1 special character, 1 digit, and have a length of 6-15.";
            public const string FullNameTip = "Real name must contain only letters, and have a length of 6-20.";
            public const string ConfirmPasswordTip = "This field must match the password.";
            public const string GoodTip = "This field looks fine!";

        }

        public static class Errors
        {
            public static class Register
            {
                public const string RegisteredSuccessfully = "Account registered successfully!";
                public const string UsernameAlreadyExists = "An account with this username already exists!";
                public const string EmailAlreadyExists = "An account with this email adress already exists!";
            }
            public static class Login
            {
                public const string UsernameNotFound = "An account with this username could not be found!";
                public const string PasswordNotMatching = "Incorrect password for this username!";
                public const string GoodTip = "Good!";
            }
        }

        private static List<char> SpecialCharacters;

        static FieldVerification()
        {
            SpecialCharacters = new List<char> { '~', '!', '@', '#', '$', '%', '^', '&', '*' };

        }


        private static bool IsInsideLengthBoundaries(string field, int lowest, int highest)
        {
            if (string.IsNullOrEmpty(field)) return false;

            return field.Length >= lowest && field.Length <= highest;
        }

        private static bool ContainsSpecialCharacters(string field)
        {
            if (string.IsNullOrEmpty(field)) return false;

            foreach (char c in SpecialCharacters)
            {
                if (field.Contains(c)) return true;
            }
            return false;
        }
        private static bool ContainsCapitalLetter(string field)
        {
            return field.Any(char.IsUpper);
        }



        private static bool ContainsOnlyLettersOrWhitespaces(string field)
        {
            if (string.IsNullOrEmpty(field)) return false;
            foreach (char c in field)
            {
                if (!(char.IsLetter(c) || char.IsWhiteSpace(c))) return false;
            }
            return true;
        }
        
        public static bool AreMatching(string field1, string field2)
        {
            if (string.IsNullOrEmpty(field1) || string.IsNullOrEmpty(field2)) return false;
            return field1.Equals(field2);
        }
        
        public static bool CanBeEmailAdress(string field)
        {
            if (string.IsNullOrEmpty(field)) return false;
            string pattern = @"^[a-zA-Z0-9]+@(gmail\.com|yahoo\.com)$";
            return Regex.IsMatch(field, pattern);
        }
        public static bool CanBePassword(string field)
        {
            return IsInsideLengthBoundaries(field, 6, 15) && field.Any(char.IsLetter) && field.Any(char.IsDigit) && ContainsSpecialCharacters(field) ;
        }
        public static bool CanBeFullName (string field)
        {
            return IsInsideLengthBoundaries(field, 6, 20) && ContainsOnlyLettersOrWhitespaces(field);
        }
        public static bool CanBeUsername(string field)
        {
            return IsInsideLengthBoundaries(field, 6, 15) && ContainsCapitalLetter(field) && field.Any(char.IsDigit);
        }
    }
}
