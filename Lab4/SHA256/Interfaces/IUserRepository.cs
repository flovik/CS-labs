using SHA256.Models;

namespace SHA256Encryption.Interfaces;

public interface IUserRepository
{
    void CreateUser(User user);
    User GetUser(Guid id);
}