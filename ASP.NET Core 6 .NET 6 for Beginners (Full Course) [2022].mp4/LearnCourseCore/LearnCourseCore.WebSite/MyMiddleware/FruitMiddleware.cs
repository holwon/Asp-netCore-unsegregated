using Microsoft.Extensions.Options;

namespace LearnCourseCore.WebSite.MyMiddleware
{
    public class FruitMiddleware
    {
        private RequestDelegate _next;
        private FruitOption _option;

        public FruitMiddleware(RequestDelegate next, IOptions<FruitOption> options)
        {
            _next = next;
            _option = options.Value;
        }

        public async Task Invoke(HttpContext context,ILogger<FruitMiddleware> logger)
        {
            if (context.Request.Path == "/fruit")
            {
                /*
                 * // 这样写报 CA2254 警告, 传递给记录器 API 的消息模板不是常量。
                // logger.LogDebug($"Started process for {context.Request.Path}");
                */
                // 微软建议的写法
                logger.LogDebug("Started process for {context.Request.Path}",context.Request.Path);
                await context.Response.WriteAsync($"{_option.Name},{_option.Color}");
                logger.LogDebug("End process for {context.Request.Path}",context.Request.Path);
            }
            else
            {
                // await _next.Invoke(context);
                await _next(context);
                logger.LogDebug($"/fruit not request {context.Request.Path}");
            }

            logger.LogDebug($"/fruit was or was not request {context.Request.Path}");
            // 有这个的话, 会返回两次 next 
            // if (_next != null)
            // {
            //     await _next(context);
            // }
        }
    }
}