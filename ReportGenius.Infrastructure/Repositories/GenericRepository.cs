using Dapper;
using ReportGenius.Application.Interfaces;
using ReportGenius.Infrastructure.Database.Connection;
using System.Data;

namespace ReportGenius.Infrastructure.Repositories
{
    public sealed class GenericRepository : IGenericRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public GenericRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(
            string sql,
            object? parameters = null,
            CommandType commandType = CommandType.Text,
            CancellationToken cancellationToken = default)
        {
            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryAsync<T>(
                sql,
                parameters,
                commandType: commandType);
        }

        public async Task<T?> QueryFirstOrDefaultAsync<T>(
            string sql,
            object? parameters = null,
            CommandType commandType = CommandType.Text,
            CancellationToken cancellationToken = default)
        {
            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryFirstOrDefaultAsync<T>(
                sql,
                parameters,
                commandType: commandType);
        }

        public async Task<int> ExecuteAsync(
            string sql,
            object? parameters = null,
            CommandType commandType = CommandType.Text,
            CancellationToken cancellationToken = default)
        {
            using var connection = _connectionFactory.CreateConnection();

            return await connection.ExecuteAsync(
                sql,
                parameters,
                commandType: commandType);
        }

        public async Task<object?> ExecuteScalarAsync(
            string sql,
            object? parameters = null,
            CommandType commandType = CommandType.Text,
            CancellationToken cancellationToken = default)
        {
            using var connection = _connectionFactory.CreateConnection();

            return await connection.ExecuteScalarAsync(
                sql,
                parameters,
                commandType: commandType);
        }
    }
}
