using ReportGenius.Application.Models.Schema;

namespace ReportGenius.Application.Interfaces
{
    public interface IDatabaseSchemaProvider
    {
        Task<DatabaseSchema> GetSchemaAsync(CancellationToken cancellationToken = default);
    }
}
