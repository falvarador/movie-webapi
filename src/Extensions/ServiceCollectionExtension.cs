namespace MovieWeb.WebApi.Extension
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using MovieWeb.WebApi.Helper;
    using MovieWeb.WebApi.Infraestructure;
    using MovieWeb.WebApi.Service;
    using Swashbuckle.AspNetCore.Swagger;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using Swashbuckle.AspNetCore.SwaggerUI;
    using System;

    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddServiceConfiguration(this IServiceCollection services)
        {
            return services
                .AddTransient<IGenderService, GenderService>()
                .AddTransient<IMovieService, MovieService>()
                .AddTransient<IPersonService, PersonService>();
        }

        public static IServiceCollection AddRepositoryConfiguration(this IServiceCollection services)
        {
            return services
                .AddTransient<IMovieRepository, MovieRepository>();
        }

        public static IServiceCollection AddModelConfiguration(this IServiceCollection services)
        {
            return services            
                .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        public static IServiceCollection AddHelperConfiguration(this IServiceCollection services) 
        {
            return services
                .AddHttpContextAccessor()
                .AddTransient<ILocalFileStorage, LocalFileStorage>();
        }

        public static IServiceCollection AddDbConnectionAndProvider(this IServiceCollection services, IConfiguration configuration)
        {           
            return services
                .AddDbContext<InfraestructureContext>(options => options.UseSqlServer(configuration.GetConnectionString("SqlServer")));
        }

        public static IServiceCollection AddApiVersionWithExplorer(this IServiceCollection services)
        {
            return services
                .AddVersionedApiExplorer(options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                })
                .AddApiVersioning(options =>
                {
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.ReportApiVersions = true;
                    options.DefaultApiVersion = new ApiVersion(2, 1);
                });
        }

        public static IServiceCollection AddSwaggerOptions(this IServiceCollection services)
        {
            return services
                .AddTransient<IConfigureOptions<SwaggerOptions>, ConfigureSwaggerOptions>()
                .AddTransient<IConfigureOptions<SwaggerUIOptions>, ConfigureSwaggerUIOptions>()
                .AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>();
        }
    }
}
