using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaesarCipher.Ciphers
{
    public abstract class AbstractCipher
    {
        private protected int SubstitutionKey;

        protected virtual bool IsInAlphabet(char character)
        {
            return character is >= 'a' and <= 'z' or >= 'A' and <= 'Z';
        }

        protected virtual char ReplaceChar(char character)
        {
            var asciiFirstLetter = char.IsLower(character) ? 'a' : 'A'; //need that to subtract to make correct mod
            var replacedChar = (char) ((character + SubstitutionKey - asciiFirstLetter) % 26 + asciiFirstLetter);
            return replacedChar;
        }

        protected char[] ReplaceText(char[] message)
        {
            for (var i = 0; i < message.Length; i++)
            {
                //skip whitespaces, commas, dots, etc.
                if (!IsInAlphabet(message[i])) continue;

                //replace char with a new encrypted one
                message[i] = ReplaceChar(message[i]);
            }

            return message;
        }
    }
}
