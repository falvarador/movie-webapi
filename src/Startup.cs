namespace MovieWeb.WebApi
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using MovieWeb.WebApi.Common;
    using MovieWeb.WebApi.Extension;
    using MovieWeb.WebApi.Middleware;

    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
               .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }
        readonly string allowSpecificOrigins = "_allowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddJsonOptions(options => 
                { 
                    options.JsonSerializerOptions.PropertyNamingPolicy = 
                        SnakeCaseNamingPolicy.Instance;
                });

            // Add CORS Aplication Support
            services
                .AddCors(options =>
                {
                    options.AddPolicy(allowSpecificOrigins,
                        builder =>
                        {
                            builder
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowAnyOrigin();
                        });
                });

            // Add Custom Service Application
            services
                .AddServiceConfiguration()
                .AddRepositoryConfiguration()
                .AddModelConfiguration()
                .AddHelperConfiguration()
                .AddDbConnectionAndProvider(Configuration)
                .AddApiVersionWithExplorer()
                .AddSwaggerOptions()
                .AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            logger.AddFile(Configuration.GetSection("Logging"));

            app.UseStaticFiles();
            app.UseCors(allowSpecificOrigins);
            app.UseExceptionMiddleware();

            app.UseSwaggerMiddleware();
            app.UseStaticFiles(new StaticFileOptions
            {
                ServeUnknownFileTypes = true,
                DefaultContentType = "application/yaml",
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }       
}
