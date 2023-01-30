using System.Text;
using __JWT.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

#region Set configuration
var connectionString = configuration.GetConnectionString("DefaultConnectionString")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnectionString' not found.");
var issuer = configuration["JWT:Issuer"]?.ToString()
    ?? throw new InvalidOperationException("Configuration 'Issuer' not found.");
var audience = configuration["JWT:Audience"]?.ToString()
    ?? throw new InvalidOperationException("Configuration 'Audience' not found.");
var key = configuration["JWT:Key"]?.ToString()
    ?? throw new InvalidOperationException("Configuration 'Key' not found.");
var encryptKey = Encoding.UTF8.GetBytes(key);
#endregion

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddDbContext<AuthContext>(opts => opts.UseSqlite(connectionString));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        // 这个是由加密密钥生成的解密密钥
        IssuerSigningKey = new SymmetricSecurityKey(encryptKey)
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller?}/{action?}/{id?}");

app.MapControllers();
app.Run();
