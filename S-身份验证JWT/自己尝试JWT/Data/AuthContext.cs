using __JWT.Models;
using Microsoft.EntityFrameworkCore;

public class AuthContext : DbContext
{
    public AuthContext(DbContextOptions<AuthContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
}