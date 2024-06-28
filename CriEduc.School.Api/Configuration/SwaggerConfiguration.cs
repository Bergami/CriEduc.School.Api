using Microsoft.OpenApi.Models;
using System.Reflection;

namespace CriEduc.School.Api.Configuration
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "CriEduc School",
                    Description = "An ASP.NET Core Web API for managing School",
                    Contact = new OpenApiContact
                    {
                        Name = "Wander Vinicius Bergami",
                        Email = "wander.bergami@gmail.com"
                    }
                });

                // using System.Reflection;
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

                var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                    .Where(x => x.GetName().Name?.Contains("CriEduc.School") ?? false);

                foreach (var assembly in assemblies)
                {
                    var xml_path = Path.Combine(AppContext.BaseDirectory, $"{assembly.GetName().Name}.xml");
                    if (File.Exists(xml_path))
                    {
                        options.IncludeXmlComments(xml_path);
                    }
                }

            });

            return services;
        }
    }
}
