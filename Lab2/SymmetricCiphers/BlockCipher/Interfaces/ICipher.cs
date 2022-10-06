using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymmetricCiphers.BlockCipher.Interfaces
{
    public interface ICipher
    {
        string Encrypt(string plaintext);
        string Decrypt(string ciphertext);
    }
}
