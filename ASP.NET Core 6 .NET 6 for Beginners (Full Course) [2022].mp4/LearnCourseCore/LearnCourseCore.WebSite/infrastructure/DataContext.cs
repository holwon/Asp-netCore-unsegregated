using LearnCourseCore.WebSite.Models;
using Microsoft.EntityFrameworkCore;

namespace LearnCourseCore.WebSite.infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
    }
}
