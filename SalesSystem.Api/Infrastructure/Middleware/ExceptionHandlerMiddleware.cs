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
                    //case UnsupportedFileType:
                    //    // unsupported media type error 
                    //    response.StatusCode = (int)HttpStatusCode.UnsupportedMediaType;
                    //    break;
                    //case CannotAssignSameRoleGroup:
                    //    response.StatusCode = (int)HttpStatusCode.Conflict;
                    //    break;
                    //case SameRoleName:
                    //    response.StatusCode = (int)HttpStatusCode.Conflict;
                    //    break;
                    //case RoleExists:
                    //    response.StatusCode = (int)HttpStatusCode.Conflict;
                    //    break;
                    //case DateRange:
                    //    response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                    //    break;
                    //case DatesCannotBeNull:
                    //    response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                    //    break;
                    //case Forbidden:
                    //    response.StatusCode = (int)HttpStatusCode.Forbidden;
                    //    break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new { response.StatusCode, error?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}
