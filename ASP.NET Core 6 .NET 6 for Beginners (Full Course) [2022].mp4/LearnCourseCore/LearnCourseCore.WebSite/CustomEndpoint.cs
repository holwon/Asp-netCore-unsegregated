using LearnCourseCore.WebSite.Services;

namespace LearnCourseCore.WebSite
{
    public class CustomEndpoint
    {
        public static async Task Endpoint(HttpContext context)
        {
            // 此处 Get到的 Services与 Program.cs里Add Services有关
            // 比如:
            // builder.Services.AddSingleton<IResponseFormatter, TextResponseFormatter>();
            // builder.Services.AddSingleton<IResponseFormatter, HtmlResponseFormatter>();
            // 那么 Get到的就是 `HtmlResponseFormatter`
            
            IResponseFormatter formatter = context.RequestServices.GetRequiredService<IResponseFormatter>();

            await formatter.Format(context, "CustomEndpoint");
        }
    }
}
