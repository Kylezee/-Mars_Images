using Microsoft.Extensions.Options;
using System;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace kfe.Infrastructure.DataAccess
{
    public class ConnectionStringBuilder : IConnectionStringBuilder
    {
        private readonly ILogger _logger;
        private readonly DatabaseConnections _databaseConnections;

        public ConnectionStringBuilder(
            ILoggerFactory loggerFactory,
            IOptions<DatabaseConnections> options
            )
        {
            _logger = loggerFactory.CreateLogger<ConnectionStringBuilder>();
            _databaseConnections = options.Value;
        }

        public string Source
        {
            get { return _databaseConnections.Primary.Source; }
        }

        public string Build()
        {

            _logger.LogInformation($"Starting {nameof(Build)}");

            if (string.IsNullOrWhiteSpace(_databaseConnections.Primary.Server))
            {
                throw new ArgumentNullException(nameof(_databaseConnections.Primary.Server));
            }

            if (string.IsNullOrWhiteSpace(_databaseConnections.Primary.Database))
            {
                throw new ArgumentNullException(nameof(_databaseConnections.Primary.Database));
            }

            var builder = new SqlConnectionStringBuilder();

            builder.DataSource = _databaseConnections.Primary.Server;
            builder.InitialCatalog = _databaseConnections.Primary.Database;


            if (!string.IsNullOrWhiteSpace(_databaseConnections.Primary.UserId))
            {
                builder.UserID = _databaseConnections.Primary.UserId;
            }

            if (!string.IsNullOrWhiteSpace(_databaseConnections.Primary.Password))
            {
                builder.Password = _databaseConnections.Primary.Password;
            }

            var connectionString = string.Empty;

            _logger.LogInformation($"Source: {Source}");
            switch (Source.ToUpper())
            {
                case "SQL":
                    connectionString = builder.ToString();
                    break;
                case "SQLITE":
                    connectionString = $"Data Source={builder.DataSource};";
                    break;
                case "POSTGRESQL":
                    connectionString = $"host={builder.DataSource};Port={_databaseConnections.Primary.Port};database={builder.InitialCatalog};UserName={builder.UserID};Password={builder.Password}";
                    break;

                default:
                    connectionString = $"{builder.ToString()};Database='{_databaseConnections.Primary.Database};'";
                    break;
            }

            _logger.LogInformation($"Connection String {connectionString}");
            return connectionString;



        }
    }
}
