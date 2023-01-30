using System;
using LearnCourseCore.WebSite.Models;
using Microsoft.EntityFrameworkCore;

namespace LearnCourseCore.WebSite.infrastructure
{
    // 为了查看数据, 这里设置一个 SeedData类
    public class SeedData
    {
        public static void SeedDataBase(DataContext context)
        {
            //将上下文的任何挂起迁移应用于数据库。 如果数据库尚不存在，将创建数据库。
            //请注意，此 API 与 EnsureCreated().
            //EnsureCreated 不使用迁移来创建数据库，因此以后无法使用迁移更新创建的数据库。 
            context.Database.Migrate();

            if (!context.Products.Any() && context.Categories.Count() == 0)
            {

                Category fruits = new() { Name = "Fruits" };
                Category shirts = new() { Name = "Shirts" };

                context.Products.AddRange(
                    new Product
                    {
                        Name = "Apple",
                        // 希望实数被视为 decimal 类型，请使用后缀 m 或 M，
                        Price = 1.50M,
                        Category = fruits
                    },
                    new Product
                    {
                        Name = "Lemon",
                        Price = 2.00M,
                        Category = fruits
                    }, new Product
                    {
                        Name = "Watermelon",
                        Price = 0.50M,
                        Category = fruits
                    }, new Product
                    {
                        Name = "Grapefruit",
                        Price = 2.50M,
                        Category = fruits
                    },
                    new Product
                    {
                        Name = "Blue Shirts",
                        Price = 5.99M,
                        Category = shirts
                    }, new Product
                    {
                        Name = "black Shirts",
                        Price = 5.99M,
                        Category = shirts
                    }, new Product
                    {
                        Name = "Yellow Shirts",
                        Price = 7.99M,
                        Category = shirts
                    }, new Product
                    {
                        Name = "Red Shirts",
                        Price = 9.99M,
                        Category = shirts
                    }
                );
            }
            context.SaveChanges();
        }
    }
}
