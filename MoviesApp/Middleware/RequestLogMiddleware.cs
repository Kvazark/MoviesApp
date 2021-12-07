using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MoviesApp.Middleware
{
    public class RequestLogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLogMiddleware> _logger;

        public RequestLogMiddleware(RequestDelegate next,ILoggerFactory loggerFac)
        {
            _next = next;
            _logger = loggerFac.CreateLogger<RequestLogMiddleware>();
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path.Value.ToLower().Contains("/actors"))
            {
                _logger.LogTrace($"Actor request: {httpContext.Request.Path.Value}  Method: {httpContext.Request.Method}");
            }
            await _next(httpContext);
        }
    }
}