using System.Security.Claims;
using AuthenticationAuthorization.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAuthorization.Controllers;

[Authorize]
[Route("api/users")]
[ApiController]
public class UsersController : Controller
{

    [HttpGet]
    [Route("all")]
    [Authorize(Roles = "admin")]
    public IActionResult GetAll()
    {
        var allUsers = DummyDatabase.Users;
        return Ok(allUsers);
    }

    [HttpGet]
    public IActionResult Get()
    {
        var email = User.FindFirst(ClaimTypes.Email)?.Value;
        if (string.IsNullOrWhiteSpace(email))
            return BadRequest("Invalid token");

        var user = DummyDatabase.Users.FirstOrDefault(x => x.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

        return Ok(user);
    }
}