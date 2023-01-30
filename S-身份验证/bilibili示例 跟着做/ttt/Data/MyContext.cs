using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ttt.Data;
public class MyContext : IdentityDbContext<MyUser, MyRole, Guid>
{
    public MyContext(DbContextOptions<MyContext> options) : base(options)
    {
    }
}