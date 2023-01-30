using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using test1.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
var sqliteConnection = builder.Configuration.GetConnectionString("sqliteConnection");
var sqlserverConnection = builder.Configuration.GetConnectionString("sqlserverConnection");
builder.Services.AddDbContext<MyDbContext>(option =>
{
    option.UseSqlite(sqliteConnection);
    // option.UseSqlServer(sqlserverConnection);
});

builder.Services.AddDataProtection();
builder.Services.AddIdentityCore<MyUser>(option =>
{
    var passwordOptions = option.Password;
    passwordOptions.RequireDigit = false;
    passwordOptions.RequireLowercase = false;
    passwordOptions.RequireUppercase = false;
    passwordOptions.RequiredLength = 6;
    option.Password = passwordOptions;
    option.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
    option.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
});
#region 这种不行
// }).AddEntityFrameworkStores<MyDbContext>()
// .AddUserManager<UserManager<MyUser>>()
// .AddRoleManager<RoleManager<MyRole>>();
#endregion

// Note: 直接新建实例便好, 会自动附加. 可于构造函数中查看
IdentityBuilder identity = new(typeof(MyUser), typeof(MyRole), builder.Services);
identity.AddEntityFrameworkStores<MyDbContext>()
    .AddDefaultTokenProviders()
    .AddUserManager<UserManager<MyUser>>()
    .AddRoleManager<RoleManager<MyRole>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
