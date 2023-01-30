namespace LearnCourseCore.WebSite.Services
{
    public interface IResponseFormatter
    {
        Task Format(HttpContext context, string content);
    }
}
