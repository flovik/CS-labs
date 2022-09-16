using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CaesarCipher.Ciphers;

public abstract class AbstractCipher
{
    private protected string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private protected int SubstitutionKey;

    protected virtual bool IsInAlphabet(char character)
    {
        return character is >= 'a' and <= 'z' or >= 'A' and <= 'Z';
    }

    protected virtual char ReplaceChar(char character)
    {
        char replacedChar;
        if (char.IsLower(character))
        {
            var index = alphabet.IndexOf(char.ToUpper(character)); //take index of character in the alphabet
            replacedChar = char.ToLower(alphabet[(index + SubstitutionKey) % alphabet.Length]); //apply substitution rule
        }
        else replacedChar = alphabet[(alphabet.IndexOf(character)+ SubstitutionKey) % alphabet.Length];

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