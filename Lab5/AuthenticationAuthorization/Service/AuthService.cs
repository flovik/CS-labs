using System.Net;
using AuthenticationAuthorization.Dtos;
using AuthenticationAuthorization.Exceptions;
using AuthenticationAuthorization.Interfaces;
using AuthenticationAuthorization.Models;

namespace AuthenticationAuthorization.Service;

public class AuthService : IAuthService
{
    private readonly IJwtService _jwtService;

    public AuthService(IJwtService jwtService)
    {
        _jwtService = jwtService;
    }

    public string Login(UserLoginDto data)
    {
        var user = DummyDatabase.Users.FirstOrDefault(x => x.Email == data.Email && x.Password.Equals(data.Password, StringComparison.OrdinalIgnoreCase));
        if (user is null)
            throw new HttpResponseException(HttpStatusCode.Unauthorized, "Issues logging in?");

        return GetToken(user.Email);
    }

    private string GetToken(string email)
    {
        var user = DummyDatabase.Users.FirstOrDefault(x => x.Email == email);
        return _jwtService.GenerateToken(user);
    }
}