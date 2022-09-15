using CaesarCipher.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaesarCipher.Ciphers
{
    public class CaesarCipherWithPermutation : AbstractCipher, ICypher
    {
        public CaesarCipherWithPermutation(int substitutionKey, string permutationKey)
        {
            SubstitutionKey = substitutionKey;
            alphabet = ChangeAlphabet(permutationKey);
        }

        public string encryptMessage(char[] message)
        {
            message = ReplaceText(message);
            return new string(message);
        }

        public string decryptMessage(char[] message)
        {
            SubstitutionKey = 26 - SubstitutionKey; //change substitution key to make a full circle
            message = ReplaceText(message);
            SubstitutionKey = 26 - SubstitutionKey; //restore original value of key
            return new string(message);
        }

        //generate a new alphabet from adding permutationKey and then adding the rest letters of the alphabet
        //apart from added letters
        private string ChangeAlphabet(string permutationKey)
        {
            var secondAlphabet = new StringBuilder(permutationKey);
            foreach (var letter in alphabet)
            {
                if (!permutationKey.Contains(letter)) secondAlphabet.Append(letter);
            }

            return secondAlphabet.ToString();
        }
    }


}
