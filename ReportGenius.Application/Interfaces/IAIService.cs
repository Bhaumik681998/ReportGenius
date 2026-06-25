namespace ReportGenius.Application.Interfaces
{

    public interface IAIService
    {
        Task<string> GenerateSqlAsync(string userPrompt, CancellationToken cancellationToken = default);

        Task<string> AskAsync(string prompt, CancellationToken cancellationToken = default);
    }
}
