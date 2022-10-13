using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymmetricCiphers.StreamCipher.Interfaces
{
    public interface IStreamCipher
    {
        string Encrypt(string plaintext);
        string Decrypt(string ciphertext);
    }
}
