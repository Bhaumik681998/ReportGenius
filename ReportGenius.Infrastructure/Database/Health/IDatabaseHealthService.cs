namespace ReportGenius.Infrastructure.Database.Health
{
    public interface IDatabaseHealthService
    {
        Task<bool> CanConnectAsync(CancellationToken cancellationToken = default);

        Task<string> GetDatabaseProviderAsync();
    }
}
