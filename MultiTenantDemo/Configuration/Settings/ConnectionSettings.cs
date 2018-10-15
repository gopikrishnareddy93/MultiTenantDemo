namespace MultiTenantDemo.Configuration.Settings
{
    // Connection configuration options
    public class ConnectionSettings
    {
        // Gets or sets the database type (MsSql)
        public DatabaseType DatabaseType { get; set; }
        
        /// Gets or sets the default connection.
        public string DefaultConnection { get; set; }
    }
}
