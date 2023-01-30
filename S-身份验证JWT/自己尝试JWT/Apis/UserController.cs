using System.Security.Claims;
using __JWT.Models;
using __JWT.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace __JWT.Apis;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult GetPublic()
    {
        var message = "hello world";
        return Ok(message);
    }

    [HttpGet("admin")]
    [Authorize(Roles = $"{nameof(Role.User)}, {nameof(Role.Administrator)}")]
    public IActionResult GetAdminEndpoint()
    {
        var user = _userService.GetCurrentUser(this.User);
        if (user is null)
            return BadRequest();

        var message = $"hello world {user.UserName}, Role={user.Role}";
        return Ok(message);
    }
}