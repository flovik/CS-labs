using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using SHA256Encryption.Interfaces;

public sealed class Sha256Encryptor : ISha256Encryptor
{
    public string HashPassword(string password)
    {
        using var sha256 = System.Security.Cryptography.SHA256.Create();
        var secretBytes = Encoding.UTF8.GetBytes(password);
        var secretHash = sha256.ComputeHash(secretBytes);
        return Convert.ToHexString(secretHash);
    }
}