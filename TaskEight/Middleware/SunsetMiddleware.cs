namespace TaskFive.Middleware
{
    public class SunsetMiddleware
    {
        private readonly RequestDelegate _next;
        public SunsetMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/api/v1"))
            {
                context.Response.Headers.Append("Sunset", "Sat, 16 Aug 2027 00:00:00 GMT");
            }
            await _next(context);
        }
    }
}
