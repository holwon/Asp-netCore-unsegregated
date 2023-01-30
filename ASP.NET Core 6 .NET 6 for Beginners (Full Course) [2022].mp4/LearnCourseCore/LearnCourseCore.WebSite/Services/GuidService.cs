namespace LearnCourseCore.WebSite.Services;

public class GuidService : IResponseFormatter
{
    private readonly Guid _guid = Guid.NewGuid();

    public async Task Format(HttpContext context, string content)
    {
        await context.Response.WriteAsync($"Guid {_guid}\n {context}");
    }
}