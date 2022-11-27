using System.ComponentModel.DataAnnotations;

namespace AuthenticationAuthorization.Dtos;

public class UserLoginDto
{
    [EmailAddress]
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}