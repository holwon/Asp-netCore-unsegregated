using System.Security.Principal;
using __JWT.Models;

namespace __JWT.Services;
public interface IUserService
{
    public User? GetUser(string username);
    public User? GetCurrentUser(IPrincipal userIdentity);

}