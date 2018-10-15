using System;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MultiTenantDemo.Configuration.Settings;
using MultiTenantDemo.Data.Model;
using MultiTenantDemo.Helpers;

namespace MultiTenantDemo.Data
{
    public class DeviceContext : DbContext
    {
        public DeviceContext(DbContextOptions<DeviceContext> options, 
        IHttpContextAccessor httpContentAccessor, IOptions<ConnectionSettings> connectionOptions) : base (GetModifedOptions(options, httpContentAccessor, connectionOptions))
        {
            
        }

        private static DbContextOptions<DeviceContext> GetModifedOptions(DbContextOptions<DeviceContext> options,
            IHttpContextAccessor httpContentAccessor,IOptions<ConnectionSettings> connectionOptions)
        {
            ValidateDefaultConnection(connectionOptions);
            var connectionBuilder = new SqlConnectionStringBuilder(connectionOptions.Value.DefaultConnection);
            connectionBuilder.Remove(Constants.Database);
            connectionBuilder.Add(Constants.Database, GetDataBaseName(GetTenantId(httpContentAccessor.HttpContext)));
            var contextOptionsBuilder = new DbContextOptionsBuilder<DeviceContext>();
            contextOptionsBuilder.UseSqlServer(connectionBuilder.ConnectionString);

            return contextOptionsBuilder.Options;
        }

        public DbSet<Device> Devices { get; set; } 
        public DbSet<APIUser> APIUsers { get; set; } 
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>()
                .HasKey(contact => new { contact.DeviceId });

            modelBuilder.Entity<APIUser>(entity =>
            {
                entity.ToTable("Users");

                entity.Property(e => e.Id)
                    .HasColumnName("Id");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("CreateDate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("Password")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("Username")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Token)
                    .HasColumnName("Token")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.LoginState)
                    .HasColumnName("LoginState")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasKey(e => e.Id);
            });
        }

        private static string GetTenantId(HttpContext httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            string tenantId = httpContext.Request.Headers[Constants.TenantId].ToString();

            ValidateTenantId(tenantId);

            return tenantId;
        }
   

        private static void ValidateDefaultConnection(IOptions<ConnectionSettings> connectionOptions)
        {
            if (string.IsNullOrEmpty(connectionOptions.Value.DefaultConnection))
            {
                throw new ArgumentNullException(nameof(connectionOptions.Value.DefaultConnection));
            }
        }


        private static void ValidateTenantId(string tenantId)
        {
            if (tenantId == null)
            {
                throw new ArgumentNullException(nameof(tenantId));
            }

            if (!Guid.TryParse(tenantId, out _))
            {
                throw new ArgumentNullException(nameof(tenantId));
            }
        }

        private static string GetDataBaseName(string tenantId)
        {
            var dataBaseName = Constants.TenantConfigurationDictionary[Guid.Parse(tenantId)];

            if (dataBaseName == null)
            {
                throw new ArgumentNullException(nameof(dataBaseName));
            }

            return dataBaseName;
        }
    }
}
