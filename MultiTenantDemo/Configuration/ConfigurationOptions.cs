using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MultiTenantDemo.Configuration.Settings;
using MultiTenantDemo.Helpers;

namespace MultiTenantDemo.Configuration
{
    public static class ConfigurationOptions
    {
        public static void ConfigureService(IServiceCollection services, IConfigurationRoot configuration)
        {
            services.Configure<ConnectionSettings>(configuration.GetSection(Constants.ConnectionStrings));
        }
    }
}