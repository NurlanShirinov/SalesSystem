using SalesSystem.Core.Exceptions;
using System.Net;
using System.Text.Json;

namespace SalesSystem.Api.Infrastructure.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case NotFound:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                  
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new { response.StatusCode, error?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}
