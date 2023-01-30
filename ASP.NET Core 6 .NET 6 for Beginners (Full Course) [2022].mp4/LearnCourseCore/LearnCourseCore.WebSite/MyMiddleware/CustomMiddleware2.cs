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

        //���� /middleware2�Ļ�, guid�ǲ���ˢ�µ�
        //��Ϊ�µķ������ֻ���ڽ��������ϵʱ�Żᴴ��
        //����������ʹ��ʱ���Ͳ����ͷ�������������
        // ����Ӧ�÷��� /endpoint
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
