namespace MovieWeb.WebApi.Middleware
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using MovieWeb.WebApi.Common;
    using MovieWeb.WebApi.Model;
    using System;
    using System.Text.Json;
    using System.Threading.Tasks;

    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {           
            int statusCode;
            
            switch (ex)
            {
                case ValidationException _:
                case ArgumentNullException _:
                case ArgumentException _:
                case NullReferenceException _:
                    statusCode = StatusCodes.Status400BadRequest;
                    _logger.LogWarning(CreateMessage(context, ex));
                    break;
                default:
                    statusCode = StatusCodes.Status500InternalServerError;
                    _logger.LogError(CreateMessage(context, ex));
                    break;
            }

            var result = new BaseResponse() 
            { 
                IsSuccessful = false, 
                Message = ex.Message,
                HttpStatusCode = statusCode
            };
            
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = ContentTypes.Json;
            await context.Response.WriteAsync(JsonSerializer.Serialize(result));
        }

        private string CreateMessage(HttpContext context, Exception ex)
        {
            var message = $"Exception caught in global error handler, exception message: {ex.Message}";

            if (ex.InnerException != null)
                message = $"{message} Inner exception message: {ex.InnerException.Message}";

            return $"{message} RequestId: {context.TraceIdentifier}";
        }
    }

    public static class ExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
