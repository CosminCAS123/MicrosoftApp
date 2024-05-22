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
