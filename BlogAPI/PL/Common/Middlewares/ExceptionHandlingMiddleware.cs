using System.Net;

namespace BlogAPI.PL.Common.Middlewares
{
    public class BusinessException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public BusinessException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
    record ErrorResponse(int Status, string Detail);

    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (BusinessException ex)
            {
                await HandleExceptionAsync(
                    httpContext,
                    ex.Message,
                    ex.StatusCode);
            }
            catch (Exception)
            {
                await HandleExceptionAsync(
                    httpContext,
                    "Internal Server Error",
                    HttpStatusCode.InternalServerError);
            }
        }

        private async Task HandleExceptionAsync(
            HttpContext context,
            string message,
            HttpStatusCode httpStatusCode)
        {
            HttpResponse response = context.Response;

            response.StatusCode = (int)httpStatusCode;

            var errorResponse = new ErrorResponse((int)httpStatusCode, message);

            await response.WriteAsJsonAsync(errorResponse);
        }
    }
}
