using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SU.API
{
    public class PerformanceLoggerMiddleware
    {
        private readonly RequestDelegate _next;

        public PerformanceLoggerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var timer = Stopwatch.StartNew();
            Debug.WriteLine("Summer University - Request start");
            await _next(context);
            Debug.WriteLine($"Summer University - Request end - {timer.ElapsedMilliseconds}ms");
        }
    }

    public static class ApplicationBuilderExtensions
    {
        public static void UsePerformanceLogger(this IApplicationBuilder app)
        {
            app.Use(next => new PerformanceLoggerMiddleware(next).Invoke);
        }
    }
}
