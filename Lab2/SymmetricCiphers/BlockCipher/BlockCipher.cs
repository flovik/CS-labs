using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SymmetricCiphers.BlockCipher.Extensions;
using SymmetricCiphers.BlockCipher.Helpers;
using SymmetricCiphers.BlockCipher.Interfaces;

namespace SymmetricCiphers.BlockCipher
{
    public class BlockCipher : ICipher
    {
        private readonly InitialPermutation _initialPermutation;

        public BlockCipher()
        {
            _initialPermutation = new InitialPermutation();
        }

        public string Encrypt(string plaintext)
        {
            //char is 1 byte long, so we encrypt/decrypt 8 bytes at a time (64 bits blocks)
            var result = string.Empty;

            //split text into block of 8
            var blockText = plaintext.SplitInto(8).ToList();

            //add padding to last string
            if (blockText.Last().Length != 8)
            {
                blockText[^1] = blockText[^1].PadRight(8);
            }

            foreach (var block in blockText)
            {
                //Initial Permutation
                (string leftPlainText, string rightPlainText) = _initialPermutation.Permutation(block);

            }

            return result;
        }

        public string Decrypt(string ciphertext)
        {
            throw new NotImplementedException();
        }
    }
}
