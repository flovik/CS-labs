﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymmetricCiphers.BlockCipher.Helpers
{
    public static class Utils
    {
        public static byte[] ConvertToByteArray(string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }

        public static string ToBinary(byte[] data)
        {
            return string.Join("", data.Select(byt => Convert.ToString(byt, 2).PadLeft(8, '0')));
        }

        public static string ChangeBits(string binaryString, int[,] matrix)
        {
            var result = string.Empty;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    var index = matrix[i, j];
                    result += binaryString[index - 1];
                }
            }

            return result;
        }

        public static string StringBitsToString(string bits)
        {
            var bytes = new List<byte>();

            for (int i = 0; i < bits.Length; i += 8)
            {
                bytes.Add(Convert.ToByte(bits.Substring(i, 8), 2));
            }
            return Encoding.UTF8.GetString(bytes.ToArray());
        }

        public static string BinaryStringToHexString(string binary)
        {
            if (string.IsNullOrEmpty(binary))
                return binary;

            var result = new StringBuilder(binary.Length / 8 + 1);

            var mod4Len = binary.Length % 8;
            if (mod4Len != 0)
            {
                // pad to length multiple of 8
                binary = binary.PadLeft(((binary.Length / 8) + 1) * 8, '0');
            }

            for (int i = 0; i < binary.Length; i += 8)
            {
                var eightBits = binary.Substring(i, 8);
                result.Append($"{Convert.ToByte(eightBits, 2):X2}");
            }

            return result.ToString();
        }
    }
}
