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
            alphabet = alphabet.Insert(0, permutationKey);
        }

        public string encryptMessage(char[] message)
        {
            message = ReplaceText(message);
            return new string(message);
        }

        public string decryptMessage(char[] encryptedMessage)
        {
            throw new NotImplementedException();
        }
    }


}
