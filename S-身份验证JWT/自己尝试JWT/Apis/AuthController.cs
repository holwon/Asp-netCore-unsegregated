using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using __JWT.Models;
using __JWT.Services;
using JWT.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace __JWT.Apis;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly AuthContext _context;
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration, AuthContext context, IUserService userService)
    {
        _configuration = configuration;
        _context = context;
        _userService = userService;
    }

    [HttpPost("register"), AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] UserDto request)
    {
        var result = CheckUserAlready(request.UserName);
        if (result)
            return BadRequest("User already");

        CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

        // 这里只是在思考数据库是直接存放 byte还是转成 base64
        // 不过最后还是存放了原本的 byte
        string hash = Convert.ToBase64String(passwordHash);
        string salt = Convert.ToBase64String(passwordSalt);

        User user = new()
        {
            UserName = request.UserName,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Role = nameof(Role.User)
        };
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Register), new { id = user.Id }, user);
    }

    [HttpPost("Login")]
    public IActionResult Login([FromBody] UserDto request)
    {
        var user = _userService.GetUser(request.UserName);
        if (user is null)
            return NotFound("User is not registered");

        bool isPass = CheckUserPassword(request.Password, user);
        if (!isPass)
            return BadRequest("Password is incorrect");

        string jwt = CreateToken(user);
        return Ok(jwt);
    }

    private bool CheckUserPassword(string password, User user)
        => VerifyPasswordHash(password, user.PasswordHash!, user.PasswordSalt!);

    private string CreateToken(User user)
    {
        var issuer = _configuration["JWT:Issuer"]?.ToString()
            ?? throw new InvalidOperationException("Configuration 'Issuer' not found.");
        var audience = _configuration["JWT:Audience"]?.ToString()
            ?? throw new InvalidOperationException("Configuration 'Audience' not found.");
        var key = _configuration["JWT:Key"]?.ToString()
            ?? throw new InvalidOperationException("Configuration 'Key' not found.");
        // Header 自动生成

        // Payload
        List<Claim> claims = new()
        {
            new(ClaimTypes.Name,user.UserName),
            new(ClaimTypes.Role,user.Role),
            new(ClaimTypes.Email,user.Email ?? "")
        };

        // VERIFY SigNature
        var enkey = Encoding.UTF8.GetBytes(key);
        var securityKey = new SymmetricSecurityKey(enkey);
        var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        // Generate Token
        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(5),
            signingCredentials: credential
        );
        string jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }

    private bool CheckUserAlready(string name)
    {
        var result = _context.Users.Any(u => u.UserName == name);
        return result;
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA256();
        passwordSalt = hmac.Key;// 不必担心 key 存储数据库会导致密码逆向破解, hash过程会损失一部分信息.理论上不可逆
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new HMACSHA256(passwordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(passwordHash);
    }
}