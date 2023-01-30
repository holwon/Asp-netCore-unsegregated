using RestSharp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string environment = builder.Environment.ContentRootPath;

builder.Services.AddHttpClient("RestSharp Client", client
    => client.BaseAddress = new(environment));

builder.Services.AddScoped(sp =>
{
    var httpClient = sp.GetRequiredService<IHttpClientFactory>()
    .CreateClient("RestSharp Client");
    RestClient restClient = new(httpClient);
    return restClient;
});

// builder.Services.AddSingleton(typeof(RestClient), sp => sp.GetRequiredService<IHttpClientFactory>()
// .CreateClient("RestSharp Client"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
