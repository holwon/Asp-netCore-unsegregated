namespace WebAPI1
{
    public static class ServiceCollectionExtensions
    {
        // 参数带个 this就表示这是个拓展方法
        //对 IServiceCollection 类型进行添加拓展方法
        public static IServiceCollection AddMongoDB(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}
