using AuthenticationAuthorization.Models;

namespace AuthenticationAuthorization.Interfaces;

public interface IJwtService
{
    string GenerateToken(User user);
}