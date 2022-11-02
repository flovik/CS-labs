using System;
using System.Text;

namespace RSA.RsaCipher.Helpers;

public static class Utils
{
    public static string IntToHexList(int[] plainTextBits)
    {
        var sb = new StringBuilder();
        foreach (var plainTextBit in plainTextBits)
        {
            sb.Append(plainTextBit.ToString("x8"));
        }

        return sb.ToString();
    }

    public static int[] StringToInts(string input)
    {
        int[] result = new int[input.Length];
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = Convert.ToInt32(input[i]);
        }

        return result;
    }

    public static string IntsToString(int[] input)
    {
        var sb = new StringBuilder();
        foreach (var t in input)
        {
            sb.Append(Convert.ToChar(t));
        }
        return sb.ToString();
    }

    public static int[] HexToInts(string hexString)
    {
        var list = new List<string>();
        for (int i = 0; i < hexString.Length; i += 8)
        {
            list.Add(hexString.Substring(i, 8));
        }

        int[] result = new int[list.Count];
        for (int i = 0; i < list.Count; i++)
        {
            result[i] = int.Parse(list[i], System.Globalization.NumberStyles.HexNumber);
        }

        return result;
    }
}