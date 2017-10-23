using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace OBLMobileAppApi.Class
{
    public class RandomString
    {

        private static int DEFAULT_MIN_PASSWORD_LENGTH = 8;
        private static int DEFAULT_MAX_PASSWORD_LENGTH = 15;


        private static string PASSWORD_CHARS_LCASE = "abcdefgijkmnopqrstwxyz";
        private static string PASSWORD_CHARS_UCASE = "ABCDEFGHJKLMNPQRSTWXYZ";
        private static string PASSWORD_CHARS_NUMERIC = "23456789";
        private static string PASSWORD_CHARS_SPECIAL = "";

        public static string Generate()
        {
            return Generate(DEFAULT_MIN_PASSWORD_LENGTH,
                            DEFAULT_MAX_PASSWORD_LENGTH);
        }

        public static string Generate(int length)
        {
            return Generate(length, length);
        }


        public static string Generate(int minLength, int maxLength)
        {

            if (minLength <= 0 || maxLength <= 0 || minLength > maxLength)
                return null;

            char[][] charGroups = new char[][] 
        {
            PASSWORD_CHARS_LCASE.ToCharArray(),
            PASSWORD_CHARS_UCASE.ToCharArray(),
            PASSWORD_CHARS_NUMERIC.ToCharArray(),
            PASSWORD_CHARS_SPECIAL.ToCharArray()
        };
            Random random = new Random();
            string input = PASSWORD_CHARS_LCASE + PASSWORD_CHARS_UCASE + PASSWORD_CHARS_NUMERIC;
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < maxLength; i++)
            {
                ch = input[random.Next(0, input.Length)];
                builder.Append(ch);
            }
            return builder.ToString();
        }

        public static string Generate(int minLength, int maxLength, bool isNumeric)
        {

            if (minLength <= 0 || maxLength <= 0 || minLength > maxLength)
                return null;

            Random random = new Random();
            string input = string.Empty;
            if (!isNumeric)
                input = PASSWORD_CHARS_LCASE + PASSWORD_CHARS_UCASE + PASSWORD_CHARS_NUMERIC;
            else
                input = PASSWORD_CHARS_NUMERIC;
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < maxLength; i++)
            {
                ch = input[random.Next(0, input.Length)];
                builder.Append(ch);
            }
            return builder.ToString();
        }
    }
}