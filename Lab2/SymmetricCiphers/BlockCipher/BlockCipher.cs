using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SymmetricCiphers.BlockCipher.Extensions;
using SymmetricCiphers.BlockCipher.Helpers;
using SymmetricCiphers.BlockCipher.Interfaces;
using static SymmetricCiphers.BlockCipher.Helpers.Utils;

namespace SymmetricCiphers.BlockCipher
{
    public class BlockCipher : ICipher
    {
        private readonly InitialPermutation _initialPermutation;
        private readonly KeyGenerator _keyGenerator;
        private readonly Round _round = new();
        private readonly SBoxSubstitution _sBoxSubstitution = new();
        private readonly FinalPermutation _finalPermutation = new();

        public BlockCipher(string key)
        {
            _initialPermutation = new InitialPermutation();
            _keyGenerator = new KeyGenerator(key);
        }

        public List<string> Encrypt(string plaintext, int encrypt = 0)
        {
            //split text into block of 8
            var blockText = plaintext.SplitInto(8).ToList();

            //add padding to last string
            if (blockText.Last().Length != 8)
            {
                //PKCS7
                var padLength = 8 - (blockText.Last().Length % 8);
                blockText[^1] = blockText[^1].PadRight(8, Convert.ToChar(padLength));
            }

            for (int i = 0; i < blockText.Count; i++)
            {
                //transform the block of 8 bytes into 64 bits
                blockText[i] = ToBinary(ConvertToByteArray(blockText[i]));
            }
            
            blockText = RunEncryption(blockText, encrypt);
            return blockText;
        }

        public List<string> Decrypt(List<string> ciphertext)
        {
            for (int i = 0; i < ciphertext.Count; i++)
            {
                ciphertext[i] = HexStringToBinary(ciphertext[i]);
            }

            ciphertext = RunEncryption(ciphertext, 15);
            return ciphertext;
        }

        private List<string> RunEncryption(List<string> blockText, int encrypt = 0)
        {
            //char is 1 byte long, so we encrypt/decrypt 8 bytes at a time (64 bits blocks)
            var result = new List<string>();

            foreach (var block in blockText)
            {
                //Initial Permutation
                (string leftPlainText, string rightPlainText) = _initialPermutation.Permutation(block);
                var temp = string.Empty;

                //Perform 16 rounds on block
                for (int i = 0; i < 16; i++)
                {
                    //expand rightPlainText to match 48bit key
                    var rightPlainTextExpanded = _round.ExpandBlock(rightPlainText);

                    //xor with a key
                    var xor = _round.Xor(rightPlainTextExpanded, _keyGenerator.KeyList[Math.Abs(i - encrypt)]);

                    //SBox substitution
                    temp = _sBoxSubstitution.Permute(xor);

                    //round permutation
                    var temp2 = _round.PermuteBlock(temp);

                    //xor with leftPlainText
                    xor = _round.Xor(leftPlainText, temp2);

                    //swap LFT with RPT
                    leftPlainText = rightPlainText;
                    rightPlainText = xor;
                }

                result.Add(_finalPermutation.Permute(rightPlainText + leftPlainText));
            }

            for (int i = 0; i < result.Count; i++)
            {
                result[i] = BinaryStringToHexString(result[i]);
            }

            return result;
        }
    }
}
