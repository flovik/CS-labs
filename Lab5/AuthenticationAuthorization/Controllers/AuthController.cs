using AuthenticationAuthorization.Dtos;
using AuthenticationAuthorization.Exceptions;
using AuthenticationAuthorization.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAuthorization.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/auth")]
public class AuthController : Controller
{
    private readonly IAuthService _authService;


    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [Route("login")]
    public IActionResult Login(UserLoginDto data)
    {
        try
        {
            var token = _authService.Login(data);
            return Ok(token);
        }
        catch (HttpResponseException e)
        {
            Console.WriteLine(e);
            return Unauthorized(e.ErrorMessage);
        }
    }
}