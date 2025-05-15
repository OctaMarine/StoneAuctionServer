using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StoneActionServer.WebApi.Controllers;

[ApiController]
[Route("api/v1")]
public class AccountController : ControllerBase
{
    [Authorize]
    [HttpGet("userdata")]
    public async Task<IActionResult> GetUserData()
    {
        return Ok("data");
    }
}