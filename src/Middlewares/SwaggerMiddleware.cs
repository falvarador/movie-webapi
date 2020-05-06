namespace MovieWeb.WebApi.Middleware
{
    using Microsoft.AspNetCore.Builder;

    public static class SwaggerMiddlewareExtension
    {
        public static void UseSwaggerMiddleware(this IApplicationBuilder builder)
        {
            builder.UseSwagger();
            builder.UseSwaggerUI();
        }
    }
}
