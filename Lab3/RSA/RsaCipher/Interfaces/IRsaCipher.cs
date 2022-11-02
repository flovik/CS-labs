namespace RSA.RsaCipher.Interfaces;

public interface IRsaCipher
{
    string Encrypt(string plaintext);
    string Decrypt(string ciphertext);
}