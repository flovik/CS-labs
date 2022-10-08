using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymmetricCiphers.BlockCipher.Interfaces
{
    public interface ICipher
    {
        List<string> Encrypt(string plaintext, int encrypt = 0);
        List<string> Decrypt(List<string> ciphertext);
    }
}
