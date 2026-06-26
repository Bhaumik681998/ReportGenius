
using ReportGenius.Application.DTOs.AI;

namespace ReportGenius.Application.Interfaces
{
    public interface IAIQueryService
    {
        Task<GenerateSqlResponse> GenerateQueryAsync(GenerateSqlRequest request,CancellationToken cancellationToken = default);
    }
}
