using CaesarCipher.Interfaces;
using CaesarCipher;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaesarCipher.Ciphers
{
    public class CaesarCipherr : AbstractCipher, ICypher
    {
        public CaesarCipherr(int substitutionKey)
        {
            SubstitutionKey = substitutionKey;
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

    }
}
