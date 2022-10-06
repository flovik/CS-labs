﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public (string, string) Permutation(string block)
        {
            //transform the block of 8 bytes into 64 bits
            var binaryString = ToBinary(ConvertToByteArray(block));
            binaryString = ChangeBits(binaryString);
            return (binaryString[..32], binaryString[32..^1]);
        }

        private byte[] ConvertToByteArray(string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }

        private string ToBinary(byte[] data)
        {
            return string.Join("", data.Select(byt => Convert.ToString(byt, 2).PadLeft(8, '0')));
        }

        private string ChangeBits(string binaryString)
        {
            var result = string.Empty;
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < Matrix.GetLength(0); j++)
                {
                    result += binaryString[Matrix[i, j] - 1];
                }
            }

            return result;
        }



        /*
        in initial permutation makes transposition of bits. 58th bit replaces the 1st bit, 50th bit replaces the 2nd etc
        */
    }
}
