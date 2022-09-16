using CaesarCipher.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaesarCipher.Ciphers;

public class CaesarCipherWithPermutation : CaesarCipherr
{
    public CaesarCipherWithPermutation(int substitutionKey, string permutationKey)
    {
        SubstitutionKey = substitutionKey;
        alphabet = ChangeAlphabet(permutationKey);
    }

    public void PrintNewAlphabet()
    {
        Console.WriteLine(alphabet);
    }

    //generate a new alphabet from adding permutationKey and then adding the rest letters of the alphabet
    //apart from added letters
    private string ChangeAlphabet(string permutationKey)
    {
        var secondAlphabet = new StringBuilder(permutationKey);
        foreach (var letter in alphabet)
        {
            if (!permutationKey.Contains(letter)) secondAlphabet.Append(letter);
        }

        return secondAlphabet.ToString();
    }
}