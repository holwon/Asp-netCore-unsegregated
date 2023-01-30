using LearnCourseCore.WebSite;
using LearnCourseCore.WebSite.infrastructure;
using LearnCourseCore.WebSite.MyMiddleware;
using LearnCourseCore.WebSite.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

// 注: 利用 git log 来查看`学习过程`

var builder = WebApplication.CreateBuilder(args);
#region 添加 Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion

builder.Services.AddControllers();
// Add services to the container.
// 要先添加依赖注入
//builder.Services.Configure<FruitOption>(options =>
//{
//    options.Name = "watermeion(西瓜皮)";
//});

builder.Services.AddHsts(options =>
{
    // 指定`浏览器`只发出 `https`请求的时间段, 一般是 30天
    options.MaxAge = TimeSpan.FromDays(1);
    options.IncludeSubDomains = true;
});

#region EF Core Services
// builder.Services.AddSqlServer()
builder.Services.AddDbContext<DataContext>(options =>
{
    // DbConnection不行, 如何让文件生成到当前目录下
    // options.UseSqlServer(builder.Configuration["ConnectionStrings:DbConnection"]);
    options.UseSqlServer(builder.Configuration["ConnectionStrings:DbConnection1"]);
});
#endregion


#region SessionsService
// builder.Services.AddDistributedMemoryCache();

// builder.Services.AddSession(options =>
// {
//     options.IdleTimeout = TimeSpan.FromMinutes(30);
//     options.Cookie.IsEssential = true;// 确保 cookies总是有效
// });
#endregion


//builder.Services.AddScoped<IResponseFormatter, GuidService>();

// 单例并不适合所有情况，这就是asp支持作用域服务和临时服务的原因

// builder.Services.AddSingleton<IResponseFormatter, TextResponseFormatter>();
// builder.Services.AddSingleton<IResponseFormatter, HtmlResponseFormatter>();

//builder.Services.AddTransient<IResponseFormatter, GuidService>();

var app = builder.Build();
#region SessionsService
//app.UseSession();// 启用 Session
#endregion

app.MapControllers();
// var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<DataContext>();
// SeedData.SeedDataBase(context);


//app.Use(async (context, next) =>
//{
//    if (!context.Response.HasStarted)
//    {
//        // 设定页面字符集为 UTF-8
//        context.Response.ContentType = "text/plain;charset=utf-8";
//    }

//    //await next();
//    //await context.Response.WriteAsync($"\n Response.StatusCode is '{context.Response.StatusCode}'");
//    if (context.Request.Path == "/short")
//    {
//        await context.Response.WriteAsync("Requerst short-circuited(环路)");
//    }
//    else
//    {
//        await next();
//    }
//});

if (app.Environment.IsDevelopment())
{
    // 强制https
    app.UseHsts();
    #region 添加 Swagger
    app.UseSwagger();
    app.UseSwaggerUI();
    #endregion
}

// app.MapGet("/", () => "Hello World! 测试字符集: (环)");


app.UseStaticFiles();// 添加静态文件, 如 wwwroot文件夹下的 html文件
app.UseHttpsRedirection();// 强制重定向到 https
// app.UseExceptionHandler("/Error");

//app.Start();
app.Run();
