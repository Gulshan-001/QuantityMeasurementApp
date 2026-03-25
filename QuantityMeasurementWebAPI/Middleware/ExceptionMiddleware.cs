using System.Net;
using System.Text.Json;

namespace QuantityMeasurementConsole.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private static Task HandleException(HttpContext context, Exception ex)
        {
            int statusCode = ex is ArgumentException ? 400 : 500;

            var response = new
            {
                timestamp = DateTime.UtcNow,
                status = statusCode,
                error = statusCode == 400 ? "Bad Request" : "Internal Server Error",
                message = ex.Message,
                path = context.Request.Path
            };

            var json = JsonSerializer.Serialize(response);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            return context.Response.WriteAsync(ex.InnerException?.Message ?? ex.Message);
        }
    }
}