namespace kfe.Infrastructure.DataAccess
{
    public class DatabaseConnections
    {
        public DbConnectionInfo Primary { get; set; }
    }

    public class DbConnectionInfo
    {
        public string Source { get; set; }
        public string Server { get; set; }
        public string Port { get; set; }
        public string Database { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
    }
}
