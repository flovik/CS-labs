using CaesarCipher.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaesarCipher.Ciphers
{
    //public class CaesarCipherWithPermutation : AbstractCipher, ICypher
    //{
    //    private readonly string PermutationKey;

    //    public CaesarCipherWithPermutation(int substitutionKey, string permutationKey)
    //    {
    //        SubstitutionKey = substitutionKey;
    //        PermutationKey = permutationKey;
    //    }

    //    public string encryptMessage(char[] message)
    //    {
    //        var alphabet = Constants.alphabet;
    //        var size = Constants.size;

    //        for (int i = 0; i < message.Length; i++)
    //        {
    //            //skip whitespaces, commas, dots, etc.
    //            if (SkipJunk(message[i]))
    //                continue;



    //        }
    //        return "";
    //    }

    //    public string decryptMessage(char[] encryptedMessage)
    //    {
    //        Console.WriteLine(encryptedMessage);
    //        return "";
    //    }
    //}
}
