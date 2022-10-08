using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SymmetricCiphers.BlockCipher.Helpers.Utils;

namespace SymmetricCiphers.BlockCipher.Helpers
{
    public class KeyGenerator
    {
        private string Key;
        public List<string> KeyList { get; private set; } = new();

        //56 bits only, discard every 8th bit
        private static readonly int[,] KeyMatrix1 =
        {
            { 57, 49, 41, 33, 25, 17, 9 },
            { 1, 58, 50, 42, 34, 26, 18 },
            { 10, 2, 59, 51, 43, 35, 27 },
            { 19, 11, 3, 60, 52, 44, 36 },
            { 63, 55, 47, 39, 31, 23, 15 },
            { 7, 62, 54, 46, 38, 30, 22 },
            { 14, 6, 61, 53, 45, 37, 29 },
            { 21, 13, 5, 28, 20, 12, 4 }
        };

        //48 bits only to generate a new key, also some bits are dropped
        private static readonly int[,] KeyMatrix2 =
        {
            { 14, 17, 11, 24, 1, 5, 3, 28 },
            { 15, 6, 21, 10, 23, 19, 12, 4},
            { 26, 8, 16, 7, 27, 20, 13, 2},
            { 41, 52, 31, 37, 47, 55, 30, 40},
            { 51, 45, 33, 48, 44, 49, 39, 56},
            { 34, 53, 46, 42, 50, 36, 29, 32}
        };

        //here we deduce how many shifts we need to make on key
        private static readonly int[] ShiftRounds = { 1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1 };

        public KeyGenerator(string key)
        {
            Key = key;
            GenerateKeys();
        }

        private void GenerateKeys()
        {
            var binaryKey = ToBinary(ConvertToByteArray(Key));
            //discard every 8th bit
            binaryKey = ChangeBits(binaryKey, KeyMatrix1);
            
            //split key in left and right
            (string left, string right) = (binaryKey[..28], binaryKey[28..]);
            
            //for 16 rounds we need a new key
            for (int i = 0; i < 16; i++)
            {
                left = ShiftLeftBy(left, ShiftRounds[i]);
                right = ShiftLeftBy(right, ShiftRounds[i]);
                var key = left + right;
                var newKey = ChangeBits(key, KeyMatrix2);
                KeyList.Add(newKey);
            }
        }

        private string ShiftLeftBy(string key, int shift)
        {
            string leftSide = key[..shift];
            string rightSide = key[shift..];
            return rightSide + leftSide;
        }
    }
}
