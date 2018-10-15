using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using MultiTenantDemo.Controllers;

namespace MultiTenantDemo.Configuration
{
    /// Swagger API documentation components start-up configuration
    public static class ApiVersioningConfiguration
    {
        public static void ConfigureService(IServiceCollection services)
        {
            // Swagger API documentation
            services.AddApiVersioning(o => {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.ApiVersionReader = new HeaderApiVersionReader("api-version");
                o.Conventions.Controller<DevicesController>().HasApiVersion(new ApiVersion(1, 0));
            });
        }
    }
}