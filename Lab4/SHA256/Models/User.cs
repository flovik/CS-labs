using System.ComponentModel.DataAnnotations;

namespace SHA256.Models;

public class User
{
    public Guid UserId { get; set; }
    [Required]
    [MinLength(8)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Required]
    public string Login { get; set; }
}