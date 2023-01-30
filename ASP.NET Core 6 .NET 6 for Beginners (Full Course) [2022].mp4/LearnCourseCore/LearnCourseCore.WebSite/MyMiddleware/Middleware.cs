namespace LearnCourseCore.WebSite.MyMiddleware;
public class Middleware
{
    //此 _next 可以理解为指向下一个 Request 的指针
    private RequestDelegate _next;
    public Middleware(RequestDelegate next)
    {
        _next = next;
    }
    public Middleware()
    {
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Method == HttpMethods.Get && context.Request.Query["test"] == "true")
        {
            if (!context.Response.HasStarted)
            {
                context.Response.ContentType = "text/plain";
            }
            await context.Response.WriteAsync("Class - Middleware \n");
        }
        if (_next != null)
        {
            await _next(context);
        }
    }
}
