using Microsoft.AspNetCore.Mvc;
using StoneActionServer.BusinessLogic.Services;

namespace StoneActionServer.WebApi.Controllers;

[ApiController]
[Route("api/v1")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromForm]string userName, [FromForm]string password, [FromForm]string email)
    {
        var result = await _authService.Register(userName,password,email);
        if (!result)
        {
            return BadRequest();
        }
        return Ok("register" +result);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromForm]string userName,[FromForm] string password)
    {
        Console.WriteLine("Login ...");
        var token = await _authService.Login(userName, password);
        if (token == string.Empty)
        {
            return BadRequest();
        }
        return Ok(token);
    }
}