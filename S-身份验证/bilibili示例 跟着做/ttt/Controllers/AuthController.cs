using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ttt.Data;

namespace ttt.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<MyUser> _userManager;
    private readonly RoleManager<MyRole> _roleManager;
    private readonly MyContext _context;
    public AuthController(UserManager<MyUser> userManager, RoleManager<MyRole> roleManager, MyContext context)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _context = context;
    }

    [HttpPost("register"), AllowAnonymous]
    public async Task<ActionResult> Register([FromBody] UserDto request)
    {
        const string RoleName = "User";
        #region 创建角色
        bool roleIsExist = await _roleManager.RoleExistsAsync(RoleName);
        if (!roleIsExist)
        {
            MyRole role = new() { Name = RoleName };
            var roleResult = await _roleManager.CreateAsync(role);

            if (!roleResult.Succeeded)
                return BadRequest("Create role failed");
        }
        #endregion

        #region 创建用户
        if (string.IsNullOrEmpty(request.UserName))
            return BadRequest();

        bool userIsExist = CheckUserIsExisting(request.UserName);
        if (userIsExist)
            return BadRequest("user is exist");

        MyUser user = new() { UserName = request.UserName };
        var userResult = await _userManager.CreateAsync(user, request.Password!);
        if (userResult.Succeeded)
            return CreatedAtAction(nameof(Register), request);
        #endregion
        return BadRequest("register failed");
    }

    // [HttpPost("Login")]
    // public IActionResult Login([FromBody] UserDto request)
    // {
    //     var user = _userService.GetUser(request.UserName);

    //     if (user is null)
    //         return NotFound("User is not registered");
    //     bool isPass = CheckUserPassword(request.Password, user);

    //     if (!isPass)
    //         return BadRequest("Password is incorrect");

    //     string jwt = CreateToken(user);
    //     return Ok(jwt);
    // }

    // private bool CheckUserPassword(string password, User user)
    //     => VerifyPasswordHash(password, user.PasswordHash!, user.PasswordSalt!);

    private bool CheckUserIsExisting(string userName)
        => _userManager.Users.Any(u =>
        {
            if (string.IsNullOrEmpty(u.UserName))
                return false;
            u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase);
        });

    private bool CheckUserIsExisting(string userName)
    {

        var users = _userManager.Users;

        var user = from u in users
                   where u.UserName != null
                   where users.Any(u =>
                   u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase))
                   select u.UserName.FirstOrDefault();
    }

}