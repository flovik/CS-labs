using AuthenticationAuthorization.Dtos;

namespace AuthenticationAuthorization.Interfaces;

public interface IAuthService
{
    string Login(UserLoginDto data);
}