namespace MovieWeb.WebApi.Model
{
    using System;
    using Microsoft.OpenApi.Models;

    public class SwaggerSetting
    {
        public SwaggerSetting()
        {
            Name = "This is a new Restfull API for Movie Web";
            Info = new OpenApiInfo
            {
                Title = "Movie Web Service API",
                Description = "Movie Web Service API - Test & Documentation Page, <a href='/swagger.yaml'>download swagger.yaml</a>",
                TermsOfService = new Uri("http://www.ompixcorporation.com/"),
                Contact = new OpenApiContact 
                {
                    Name = "Customer Service",
                    Email = "alvaguez1990@gmail.com",
                    Url = new Uri("http://www.ompixcorporation.com/help/")
                },
                License = new OpenApiLicense
                {
                    Name = "Ompix. ® Derechos Reservados",
                    Url = new Uri("http://www.ompixcorporation.com/"),
                }
            };
            RoutePrefix = "docs";
        }

        public string Name { get; set; }

        public OpenApiInfo Info { get; set; }

        public string RoutePrefix { get; set; }
        
        public string RoutePrefixWithSlash =>
            string.IsNullOrWhiteSpace(RoutePrefix)
                ? string.Empty
                : RoutePrefix + "/";
    }
}