namespace MovieWeb.WebApi.Extension
{
    using Microsoft.Extensions.Options;
    using MovieWeb.WebApi.Model;
    using Swashbuckle.AspNetCore.Swagger;

    public sealed class ConfigureSwaggerOptions : IConfigureOptions<SwaggerOptions>
    {
        private readonly SwaggerSetting _settings;

        public ConfigureSwaggerOptions(IOptions<SwaggerSetting> settings)
        {
            _settings = settings?.Value ?? new SwaggerSetting();
        }

        public void Configure(SwaggerOptions options)
        {
            options.RouteTemplate = _settings.RoutePrefixWithSlash + "{documentName}/swagger.json";
        }
    }
}
