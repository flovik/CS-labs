using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaesarCipher.Interfaces
{
    public interface ICypher
    {
        public string encryptMessage(char[] message);
        public string decryptMessage(char[] encryptedMessage);
    }
}
