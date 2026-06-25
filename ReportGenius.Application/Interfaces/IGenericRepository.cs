using System.Data;

namespace ReportGenius.Application.Interfaces
{
    public interface IGenericRepository
    {
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = null, CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default);

        Task<T?> QueryFirstOrDefaultAsync<T>(string sql, object? parameters = null, CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default);

        Task<int> ExecuteAsync(string sql, object? parameters = null, CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default);

        Task<object?> ExecuteScalarAsync(string sql, object? parameters = null, CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default);
    }
}
