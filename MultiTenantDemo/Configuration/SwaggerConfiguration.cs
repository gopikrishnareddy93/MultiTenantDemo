using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.DependencyInjection;
using MultiTenantDemo.ActionFilters;
using Swashbuckle.AspNetCore.Swagger;

namespace MultiTenantDemo.Configuration
{
    public static class SwaggerConfiguration
    {
        public static void ConfigureService(IServiceCollection services)
        {
            // Swagger API documentation
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                new Info
                {
                    Title = "Multi Tenant Demo Api",
                    Version = "v1.0",
                    Description = "Dotnet core multi tenant application"
                });
                
                c.OperationFilter<TenantHeaderOperationFilter>();
            });
        }
        
        public static void Configure(IApplicationBuilder app)
        {
            
            // This will redirect default url to Swagger url
            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo Api v1.0");
            });
        }
    }
}