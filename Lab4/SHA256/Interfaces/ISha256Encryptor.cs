namespace SHA256Encryption.Interfaces;

public interface ISha256Encryptor
{
    string HashPassword(string password);
}