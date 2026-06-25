namespace ReportGenius.Infrastructure.AI.Clients
{
    public interface IOllamaClient
    {
        Task<string> GenerateAsync(string prompt, CancellationToken cancellationToken = default);
    }
}
