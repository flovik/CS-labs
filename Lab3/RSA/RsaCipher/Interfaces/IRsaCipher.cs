namespace RSA.RsaCipher.Interfaces;

public interface IRsaCipher
{
    List<string> Encrypt(string plaintext);
    string Decrypt(List<string> ciphertext);
}