using System;
using System.Text;

namespace RSA.RsaCipher.Helpers;

public static class Utils
{
    public static List<string> IntToHexList(int[] plainTextBits)
    {
        var sb = new List<string>(plainTextBits.Length);
        foreach (var plainTextBit in plainTextBits)
        {
            sb.Add(plainTextBit.ToString("X"));
        }

        return sb;
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

    public static int[] HexToInts(List<string> hexString)
    {
        int[] result = new int[hexString.Count];
        for (int i = 0; i < hexString.Count; i++)
        {
            result[i] = int.Parse(hexString[i], System.Globalization.NumberStyles.HexNumber);
        }

        return result;
    }
}