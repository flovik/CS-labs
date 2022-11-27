using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthenticationAuthorization.Interfaces;
using AuthenticationAuthorization.Models;
using AuthenticationAuthorization.Service.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthenticationAuthorization.Service;

public class JwtService : IJwtService
{
    private readonly JwtSettings _jwtSettings;


    public JwtService(IConfiguration configuration)
    {
        _jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>()!;
    }

    public string GenerateToken(User user)
    {
        var claims = GenerateClaims(user);
        var token = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.SigningKey)),
                SecurityAlgorithms.HmacSha256),
            expires: DateTime.Now.AddHours(3),
            claims: claims
        );

        var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);
        return encodedToken;
    }

    private static IEnumerable<Claim> GenerateClaims(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            new(ClaimTypes.Role, user.Role.Name)
        };

        return claims;
    }
}