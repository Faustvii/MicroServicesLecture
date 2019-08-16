using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SU.API
{
    public class SummerUniversityRequestPerformaceLoggerMiddleware
    {
        private readonly RequestDelegate _next;

        public SummerUniversityRequestPerformaceLoggerMiddleware(RequestDelegate next)
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
}
