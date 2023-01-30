using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Auth.JWT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Auth.JWT.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IConfiguration _configuration;
    public LoginController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [AllowAnonymous]
    [HttpPost]
    // public IActionResult Login(UserLogin userLogin)
    public IActionResult Login([FromBody] UserLogin userLogin)
    {
        var user = Authenticate(userLogin);
        if (user is not null)
        {
            var token = GenerateToken(user);
            return Ok(token);
        }
        return NotFound("User not found");
    }

    private string GenerateToken(User user)
    {
        // 设置服务器加密的密钥
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
        // 设置签名算法 Signature
        // var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        // 设置 token 内容
        var claims = new Claim[]{
            new Claim(ClaimTypes.NameIdentifier, user.UserName),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.GivenName, user.GivenName),
            new(ClaimTypes.Surname, user.Surname),
            new(ClaimTypes.Role, user.Role)
        };
        // 生成 token
        var token = new JwtSecurityToken(
            _configuration["JWT:Issuer:0"],
            _configuration["JWT:Audience:0"],
            claims,
            // expires: DateTime.Now.AddMinutes(15),
            expires: DateTime.Now.AddSeconds(30),
            signingCredentials: credentials
        );
        // 将 token 序列化为 JWT 并返回
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private User Authenticate(UserLogin userLogin)
    {
        var currentUser = UserConstants.Users.
        FirstOrDefault(u => u.UserName.ToLower() == userLogin.UserName.ToLower()
        && u.Password == userLogin.Password);
        if (currentUser != null)
        {
            return currentUser;
        }
        return null;
    }
}