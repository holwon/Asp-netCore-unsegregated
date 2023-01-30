using Microsoft.AspNetCore.Identity;
using TestBlazorIdentity.Shared;

namespace TestBlazorIdentity.Server.Models;

public class ApplicationUser : IdentityUser
{
    public ICollection<SuperHero> SuperHeros { get; set; } = null!;
}
