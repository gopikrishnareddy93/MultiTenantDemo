using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MultiTenantDemo.Data;
using MultiTenantDemo.Helpers;

namespace MultiTenantDemo.Configuration
{
    public static class EntityFrameworkConfiguration
    {
        public static void ConfigureService(IServiceCollection services, IConfigurationRoot configuration)
        {
            string connectionString = configuration.GetConnectionString(Constants.DefaultConnection);
            
            // Entity framework configuration
            services.AddDbContext<DeviceContext>(options => options.UseSqlServer(connectionString));
        }

    }
}