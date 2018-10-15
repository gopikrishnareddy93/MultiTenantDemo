using System;
using System.Collections.Generic;

namespace MultiTenantDemo.Helpers
{
    public static class Constants
    {
        public const string DefaultConnection = nameof(DefaultConnection);
        
        public const string ConnectionStrings = nameof(ConnectionStrings);
        
        public const string Database = nameof(Database);
        
        public const string TenantId = "tenantid";
        
        // Name of the device database tenant 1
        public const string DeviceDb = nameof(DeviceDb);
        
        // Name of the device database tenant 2
        public const string DeviceDbTenant2 = "DeviceDb-ten2";
        
        // Guid of the first tenant
        public const string Tenant1Guid = "b0ed668d-7ef2-4a23-a333-94ad278f45d7";
        
        // Guid of the second tenant
        public const string Tenant2Guid = "e7e73238-662f-4da2-b3a5-89f4abb87969";

        public const string SymmetricSecurityKey = "Oxg!%gwDD43OaG1iFGnWA;39-<:uR:I,";

        public static Dictionary<Guid, string> TenantConfigurationDictionary = new Dictionary<Guid, string>
        {
            {
                Guid.Parse(Tenant1Guid), DeviceDb
            },
            {
                Guid.Parse(Tenant2Guid), DeviceDbTenant2
            }
        };
    }
}
