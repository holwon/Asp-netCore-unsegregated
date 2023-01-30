using EFCore.Models;
using Microsoft.EntityFrameworkCore;

public class ContosoPizzaContext : DbContext
{
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<OrderDetail> OrderDetails { get; set; } = null!;

    // 看看这是什么 OrderedParallelQuery
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
        @"Data Source=(localdb)\.;Integrated Security=True;Initial Catalog=LearnEF;
        AttachDBFilename=E:\a2230\Documents\projects\ASP.Net-Core\X-学习微软教程\EFCore\App_Data\LearnEF.mdf;Timeout=15");
    }
}