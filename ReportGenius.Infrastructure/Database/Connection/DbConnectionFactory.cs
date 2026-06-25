using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Npgsql;
using ReportGenius.Infrastructure.Configurations;
using ReportGenius.Infrastructure.Database.Providers;
using System.Data;
using System.Data.SqlClient;

namespace ReportGenius.Infrastructure.Database.Connection
{
    public sealed class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly DatabaseOptions _databaseOptions;
        private readonly IConfiguration _configuration;

        public DbConnectionFactory(
            IOptions<DatabaseOptions> databaseOptions,
            IConfiguration configuration)
        {
            _databaseOptions = databaseOptions.Value;
            _configuration = configuration;
        }

        public IDbConnection CreateConnection()
        {
            var provider = GetProvider();
            var connectionString = GetConnectionString();

            return provider switch
            {
                DatabaseProvider.SqlServer => CreateSqlServerConnection(connectionString),

                DatabaseProvider.PostgreSql => CreatePostgreSqlConnection(connectionString),

                DatabaseProvider.MySql =>
                    throw new NotSupportedException("MySQL support will be added in future."),

                DatabaseProvider.Oracle =>
                    throw new NotSupportedException("Oracle support will be added in future."),

                _ =>
                    throw new NotSupportedException($"Database provider '{provider}' is not supported.")
            };
        }

        private DatabaseProvider GetProvider()
        {
            if (!Enum.TryParse(
                    _databaseOptions.Provider,
                    true,
                    out DatabaseProvider provider))
            {
                throw new InvalidOperationException(
                    $"Unsupported database provider '{_databaseOptions.Provider}'.");
            }

            return provider;
        }

        private string GetConnectionString()
        {
            var connectionString =
                _configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    "DefaultConnection is missing in appsettings.json.");
            }

            return connectionString;
        }

        private static SqlConnection CreateSqlServerConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }

        private static NpgsqlConnection CreatePostgreSqlConnection(string connectionString)
        {
            return new NpgsqlConnection(connectionString);
        }
    }
}