using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace test1.Data;

public class MyDbContext : IdentityDbContext<MyUser, MyRole, Guid>
{
    // TODO: DbContextOptions<MyDbContext>
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
    }
}