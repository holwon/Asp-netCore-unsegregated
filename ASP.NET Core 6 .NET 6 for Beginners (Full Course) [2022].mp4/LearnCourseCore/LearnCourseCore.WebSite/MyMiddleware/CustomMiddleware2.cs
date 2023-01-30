using LearnCourseCore.WebSite.Services;

namespace LearnCourseCore.WebSite.MyMiddleware
{
    public class CustomMiddleware2
    {
        private RequestDelegate _next;
        private IResponseFormatter _formatter;

        public CustomMiddleware2(RequestDelegate next,IResponseFormatter formatter)
        {
            _next = next;
            _formatter = formatter;
        }

        //访问 /middleware2的话, guid是不会刷新的
        //因为新的服务对象只有在解决依赖关系时才会创建
        //当服务被正常使用时，就不能释放这个服务的依赖
        // 所以应该访问 /endpoint
        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path == "/middleware2")
            {
                await _formatter.Format(context,$"CustomMiddleware 2");
            }
            else
            {
                await _next(context);
            }
        }
    }
}
