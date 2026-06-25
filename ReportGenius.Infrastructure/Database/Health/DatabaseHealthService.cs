using Microsoft.Extensions.Options;
using ReportGenius.Infrastructure.Configurations;
using ReportGenius.Infrastructure.Database.Connection;

namespace ReportGenius.Infrastructure.Database.Health
{
    public sealed class DatabaseHealthService : IDatabaseHealthService
    {
        private readonly IDbConnectionFactory _connectionFactory;
        private readonly DatabaseOptions _databaseOptions;

        public DatabaseHealthService(IDbConnectionFactory connectionFactory,IOptions<DatabaseOptions> databaseOptions)
        {
            _connectionFactory = connectionFactory;
            _databaseOptions = databaseOptions.Value;
        }

        public async Task<bool> CanConnectAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                using var connection = _connectionFactory.CreateConnection();

                connection.Open();

                return connection.State == System.Data.ConnectionState.Open;
            }
            catch
            {
                return false;
            }
        }

        public Task<string> GetDatabaseProviderAsync()
        {
            return Task.FromResult(_databaseOptions.Provider);
        }
    }
}
