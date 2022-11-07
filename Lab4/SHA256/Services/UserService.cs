using RSA.RsaCipher.Interfaces;
using SHA256.Models;
using SHA256Encryption.Interfaces;

namespace SHA256Encryption.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;
    private readonly IRsaCipher _rsaCipher;
    private readonly ISha256Encryptor _sha256Encryptor;

    private Guid _userId;

    public UserService(IUserRepository userRepository, IRsaCipher rsaCipher, ISha256Encryptor sha256Encryptor)
    {
        _userRepository = userRepository;
        _rsaCipher = rsaCipher;
        _sha256Encryptor = sha256Encryptor;
    }

    public void AddUser()
    {
        Console.WriteLine("Enter a login: ");
        var login = Console.ReadLine();
        Console.WriteLine("Enter a password");
        var password = Console.ReadLine();

        password = _sha256Encryptor.HashPassword(password);

        var hexEncryptedPassword = _rsaCipher.Encrypt(password);
        _userId = Guid.NewGuid();

        var user = new User
        {
            Login = login,
            Password = hexEncryptedPassword,
            UserId = _userId
        };

        _userRepository.CreateUser(user);
    }

    public bool SignatureCheck()
    {
        Console.WriteLine("Enter your password to connect: ");
        var password = Console.ReadLine();

        //take the hash of password
        password = _sha256Encryptor.HashPassword(password);

        var user = _userRepository.GetUser(_userId);
        var decryptedPassword = _rsaCipher.Decrypt(user.Password);

        return string.Equals(password, decryptedPassword, StringComparison.OrdinalIgnoreCase);
    }
}