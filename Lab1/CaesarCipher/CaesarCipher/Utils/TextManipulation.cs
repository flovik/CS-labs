using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CaesarCipher.Utils
{
    public static class TextManipulation
    {
        public static string RemoveSpecialCharacters(string input)
        {
            //removes only special characters, leaves whitespaces
            //var r = new Regex("(?:[^a-z0-9 ]|(?<=['\"])s)", 
            //RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);

            //that one removes whitespaces and special characters
            var r = new Regex("(?:[^a-z0-9]|(?<=['\"])s)",
                RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            return r.Replace(input, String.Empty);
        }
    }
}
