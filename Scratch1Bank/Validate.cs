using System;
using System.Text.RegularExpressions;

namespace Scratch1Bank
{
    public static class Validate
    {
        // choice
        public static bool Choice(string input)
        {
           
            if (string.IsNullOrEmpty(input) || input == "0")
            {
                return false;

            }
            return int.TryParse(input, out _);
        }
        // amount 

        public static bool Amount(string amount)
        {
            if (string.IsNullOrWhiteSpace(amount) || decimal.TryParse(amount, out decimal value ) && value <= 0)
            {
                return false;
            }
            return decimal.TryParse(amount, out _);
        }
        // email
        //password
        // name
        // account number

        public static bool Input(string input, string pattern)
        {
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
        }



    }


}
