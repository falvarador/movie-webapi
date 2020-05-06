namespace MovieWeb.WebApi.Extension
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using MovieWeb.WebApi.Filter;
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using MovieWeb.WebApi.Model;

    public sealed class ConfigureSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerGenOptions(IApiVersionDescriptionProvider provider)
        {
            Debug.Assert(provider != null, $"{nameof(provider)} != null");

            this._provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public void Configure(SwaggerGenOptions options)
        {
            options.DocumentFilter<YamlDocumentFilter>();
            options.OperationFilter<SwaggerOperationFilter>();
            
            options.IgnoreObsoleteActions();
            options.IgnoreObsoleteProperties();

            SetCommentsPathForSwaggerJsonAndUi(options);
            AddSwaggerDocumentForEachDiscoveredApiVersion(options);
        }

        private void AddSwaggerDocumentForEachDiscoveredApiVersion(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                var _settings = new SwaggerSetting();
                _settings.Info.Version = description.ApiVersion.ToString();

                if (description.IsDeprecated)
                {
                    _settings.Info.Description += " - DEPRECATED VERSION";
                }
                else 
                {
                    _settings.Info.Description += " - SUPPORTED VERSION";  
                }

                options.SwaggerDoc(description.GroupName, _settings.Info);
            }
        }

        private static void SetCommentsPathForSwaggerJsonAndUi(SwaggerGenOptions options)
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        }
    }
}
