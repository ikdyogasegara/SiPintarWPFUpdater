using System.Collections.Generic;
using System.Text;

namespace SiPintar.Helpers
{
    public static class NumberToRomanHelper
    {
        public static string getRomanNumber(int number, bool IsLowerCase = false)
        {
            var romanNumerals = new List<string>() { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
            var numerals = new List<int>() { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
            var romanNumeral = new StringBuilder();

            while (number > 0)
            {
                var index = numerals.FindIndex(x => x <= number);
                number -= numerals[index];
                romanNumeral.Append(romanNumerals[index]);
            }
            return IsLowerCase ? romanNumeral.ToString()?.ToLower() : romanNumeral.ToString();
        }
    }
}
