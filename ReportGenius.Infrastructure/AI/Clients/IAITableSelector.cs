using ReportGenius.Application.Models.Schema;

namespace ReportGenius.Infrastructure.AI.Clients
{
    public interface IAITableSelector
    {
        Task<IReadOnlyList<string>> SelectTablesAsync(string userPrompt, DatabaseSchema schema, CancellationToken cancellationToken = default);
    }
}
