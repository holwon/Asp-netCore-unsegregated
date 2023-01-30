using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ttt.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var sqlserverConnection = builder.Configuration.GetConnectionString("sqlserverConnection");
var sqliteConnection = builder.Configuration.GetConnectionString("sqliteConnection")
?? throw new InvalidOperationException("Configuration sqliteConnection not found.");
builder.Services.AddDbContext<MyContext>(option =>
{
    option.UseSqlite(sqliteConnection);
});

builder.Services.AddDataProtection();
builder.Services.AddIdentityCore<MyUser>(option =>
{
    var passwordOptions = option.Password;
    passwordOptions.RequireDigit = false;// 密码是否必须包含数字
    passwordOptions.RequireLowercase = false;
    passwordOptions.RequireUppercase = false;
    passwordOptions.RequiredLength = 6;
    option.Password = passwordOptions;
    // 指定验证 token 的样式, 设置为简单模式. DefaultEmailProvider生成的 token(验证码) 样式比较简单
    option.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
    // 与上方同理.
    option.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
});

// Note: 直接新建实例便好, 会自动附加. 可于构造函数中查看
IdentityBuilder identity = new(typeof(MyUser), typeof(MyRole), builder.Services);
identity.AddEntityFrameworkStores<MyContext>()
    .AddDefaultTokenProviders()
    .AddUserManager<UserManager<MyUser>>()
    .AddRoleManager<RoleManager<MyRole>>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.UseAuthentication();

app.MapRazorPages();

app.Run();
