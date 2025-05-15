using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoneActionServer.BusinessLogic.Services;

namespace StoneActionServer.WebApi.Controllers;

[ApiController]
[Route("api/v1")]
public class AccountController : ControllerBase
{
    private readonly IAuthService _authService;
    
    public AccountController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [Authorize]
    [HttpGet("userdata")]
    public async Task<IActionResult> GetUserData()
    {
        return Ok("data");
    }
    
    [HttpGet("users")]
    public async Task<IActionResult> GetUsers()
    {
        var result = await _authService.GetAllUsers();
        return Ok(result.ToArray().Length);
    }
}