using System.Security.Claims;
using System.Security.Principal;
using __JWT.Models;

namespace __JWT.Services;
public class UserService : IUserService
{
    private readonly AuthContext _context;

    public UserService(AuthContext context)
    {
        _context = context;
    }

    public User? GetCurrentUser(IPrincipal userIdentity)
    {
        // var identity = HttpContext.User.Identity as ClaimsIdentity;
        var identity = userIdentity.Identity as ClaimsIdentity;
        if (identity is null)
        {
            return null;
        }
        var userClaims = identity.Claims;
        string userName, role, email;
        try
        {
            userName = userClaims.FirstOrDefault(u => u.Type == ClaimTypes.Name)?.Value ?? throw new ArgumentNullException();
            role = userClaims.FirstOrDefault(u => u.Type == ClaimTypes.Role)?.Value ?? throw new ArgumentNullException();
            email = userClaims.FirstOrDefault(u => u.Type == ClaimTypes.Email)?.Value ?? throw new ArgumentNullException();
        }
        catch (System.Exception e)
        {
            // return BadRequest(e.Message);
            System.Console.WriteLine(e.Message);
            userName = userClaims.FirstOrDefault(u => u.Type == ClaimTypes.Name)?.Value ?? "";
            role = userClaims.FirstOrDefault(u => u.Type == ClaimTypes.Role)?.Value ?? "";
            email = userClaims.FirstOrDefault(u => u.Type == ClaimTypes.Email)?.Value ?? "";
        }
        User user = new()
        {
            UserName = userName,
            Role = role,
            Email = email
        };
        return user;
    }

    public User? GetUser(string userName)
    {
        var user = _context.Users.Where(u => u.UserName == userName).FirstOrDefault();
        return user;
    }
}