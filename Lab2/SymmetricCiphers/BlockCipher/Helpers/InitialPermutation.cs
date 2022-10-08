using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SymmetricCiphers.BlockCipher.Helpers.Utils;

namespace SymmetricCiphers.BlockCipher.Helpers
{
    public class InitialPermutation
    {
        private static readonly int[,] Matrix = {
            { 58, 50, 42, 34, 26, 18, 10, 2 },
            { 60, 52, 44, 36, 28, 20, 12, 4 },
            { 62, 54, 46, 38, 30, 22, 14, 6 },
            { 64, 56, 48, 40, 32, 24, 16, 8 },
            { 57, 49, 41, 33, 25, 17, 9, 1 },
            { 59, 51, 43, 35, 27, 19, 11, 3 },
            { 61, 53, 45, 37, 29, 21, 13, 5 },
            { 63, 55, 47, 39, 31, 23, 15, 7 }
        };

        public (string, string) Permutation(string binaryString)
        {
            binaryString = ChangeBits(binaryString, Matrix);
            return (binaryString[..32], binaryString[32..]);
        }

        /*
        in initial permutation makes transposition of bits. 58th bit replaces the 1st bit, 50th bit replaces the 2nd etc
        */
    }
}
