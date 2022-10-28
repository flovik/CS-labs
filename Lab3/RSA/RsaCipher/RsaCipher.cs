using System.Numerics;
using System.Text;
using RSA.RsaCipher.Helpers;
using RSA.RsaCipher.Interfaces;
using static RSA.RsaCipher.Helpers.Utils;
namespace RSA.RsaCipher;

public class RsaCipher : IRsaCipher
{
    private readonly (BigInteger N, BigInteger D) PrivateKey;
    private readonly (BigInteger N, BigInteger E) PublicKey;
    private readonly IKeyGenerator KeyGenerator;

    public RsaCipher()
    {
        KeyGenerator = new KeyGenerator();
        PrivateKey = KeyGenerator.GeneratePrivateKey();
        PublicKey = KeyGenerator.GeneratePublicKey();
    }

    public List<string> Encrypt(string plaintext)
    {
        //get ascii representation for each character in plaintext
        var plainTextInts = StringToInts(plaintext);
        //encrypt with public key
        var encryptedInts = EncryptDecryptMessage(plainTextInts, (int) PublicKey.E);
        //return hex encoded result list
        return IntToHexList(encryptedInts);
    }

    public string Decrypt(List<string> ciphertext)
    {
        //convert hex values to int
        var encryptedInts = HexToInts(ciphertext);
        //decrypt with private key
        var decryptedInts = EncryptDecryptMessage(encryptedInts, (int) PrivateKey.D);
        //transform ascii ints to string
        return IntsToString(decryptedInts);
    }

    private int[] EncryptDecryptMessage(int[] plaintextBytes, int key)
    {
        var result = new int[plaintextBytes.Length];

        for (int i = 0; i < plaintextBytes.Length; i++)
        {
            //c = m ^ e (mod n), m = plaintext, e = public key/private key
            //raise m to the power of key (public/private)
            var encrypted = BigInteger.Pow(plaintextBytes[i], key);
            //mod n
            encrypted %= PublicKey.N;
            result[i] = (int) encrypted;
        }

        return result;
    }
}