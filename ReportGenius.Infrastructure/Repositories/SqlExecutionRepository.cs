using Dapper;
using ReportGenius.Application.Interfaces;
using ReportGenius.Infrastructure.Database.Connection;
using System.Data;

namespace ReportGenius.Infrastructure.Repositories
{
    public sealed class SqlExecutionRepository : ISqlExecutionRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public SqlExecutionRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<dynamic>> QueryAsync(string sql, object? parameters = null, CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default)
        {
            using var connection = _connectionFactory.CreateConnection();

            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            return await connection.QueryAsync(sql, parameters, commandType: commandType);
        }

        public async Task<int> ExecuteAsync(string sql, object? parameters = null, CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default)
        {
            using var connection = _connectionFactory.CreateConnection();

            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            return await connection.ExecuteAsync(sql, parameters, commandType: commandType);
        }

        public async Task<object?> ExecuteScalarAsync(string sql, object? parameters = null, CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default)
        {
            using var connection = _connectionFactory.CreateConnection();

            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            return await connection.ExecuteScalarAsync(sql, parameters, commandType: commandType);
        }
    }
}
