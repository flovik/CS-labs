using SHA256.Context;
using SHA256.Models;
using SHA256Encryption.Interfaces;

namespace SHA256Encryption.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ShaContext _context;

    public UserRepository(ShaContext context)
    {
        _context = context;
    }

    public void CreateUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public User GetUser(Guid id)
    {
        var user = _context.Users.FirstOrDefault(x => x.UserId == id);
        return user;
    }
}