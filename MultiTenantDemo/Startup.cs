using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MultiTenantDemo.ActionFilters;
using MultiTenantDemo.Configuration;
using MultiTenantDemo.Helpers;
using MultiTenantDemo.Middlewares;

namespace MultiTenantDemo
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigurationOptions.ConfigureService(services, Configuration);

            services.AddMvc(
                options =>
                {
                    options.Filters.Add(typeof(ValidateModelStateAttribute));
                });

            Mapper.Reset();
            services.AddAutoMapper(typeof(Startup));    // Swagger API documentation
            SwaggerConfiguration.ConfigureService(services);

            EntityFrameworkConfiguration.ConfigureService(services, Configuration);
            IocContainerConfiguration.ConfigureService(services, Configuration);
            ApiVersioningConfiguration.ConfigureService(services);


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            })
                .AddJwtBearer("JwtBearer", jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constants.SymmetricSecurityKey)),

                        ValidateIssuer = false,

                        ValidateAudience = false,

                        ValidateLifetime = true, //validate the expiration and not before values in the token

                        ClockSkew = TimeSpan.FromMinutes(5) //5 minute tolerance for the expiration date
                    };

                    jwtBearerOptions.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = context =>
                        {
                            var slug = context.HttpContext.Request.Headers[Constants.TenantId].ToString();

                            if (context.SecurityToken is JwtSecurityToken accessToken && !string.IsNullOrWhiteSpace(slug))
                            {
                                if (accessToken.Claims.Where(claim => claim.Type.Equals(ClaimTypes.PrimaryGroupSid)).Any(claim => slug == claim.Value))
                                {
                                    return Task.CompletedTask;
                                }
                            }

                            context.Fail($"Invalid 'slug'");
                            return Task.CompletedTask;
                        }
                    };
                });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseCors(options =>
            {
                options.AllowAnyMethod();
                options.AllowAnyOrigin();
                options.AllowAnyHeader();
            });

            SwaggerConfiguration.Configure(app);

            app.UseMvc();
        }
    }

}
