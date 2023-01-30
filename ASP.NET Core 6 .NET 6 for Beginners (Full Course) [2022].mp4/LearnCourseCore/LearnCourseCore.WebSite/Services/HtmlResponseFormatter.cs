namespace LearnCourseCore.WebSite.Services
{
    public class HtmlResponseFormatter : IResponseFormatter
    {
        private int _responseCounter = 0;

        public async Task Format(HttpContext context, string content)
        {
            context.Response.ContentType = "text/html";
            await context.Response.WriteAsync($"Format Response {++_responseCounter}<h2>{content}</h2>)");
        }

    }
}
