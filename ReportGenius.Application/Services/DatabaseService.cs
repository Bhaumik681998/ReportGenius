using ReportGenius.Application.Interfaces;

namespace ReportGenius.Application.Services
{
    public sealed class DatabaseService
    {
        private readonly IGenericRepository _repository;

        public DatabaseService(IGenericRepository repository)
        {
            _repository = repository;
        }

        public async Task<object?> TestConnectionAsync()
        {
            return await _repository.ExecuteScalarAsync("SELECT 1");
        }
    }
}
