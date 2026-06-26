using ReportGenius.Application.DTOs.AI;
using ReportGenius.Application.Interfaces;

namespace ReportGenius.Infrastructure.AI
{
    public sealed class AIQueryService : IAIQueryService
    {
        private readonly IAIService _aiService;

        private readonly ISqlExecutionRepository _sqlExecutionRepository;

        public AIQueryService(IAIService aiService,ISqlExecutionRepository sqlExecutionRepository)
        {
            _aiService = aiService;
            _sqlExecutionRepository = sqlExecutionRepository;
        }
        public async Task<GenerateSqlResponse> GenerateQueryAsync(GenerateSqlRequest request,CancellationToken cancellationToken = default)
        {
            // Step-1 : Generate SQL using AI
            var sql = await _aiService.GenerateSqlAsync(request.Prompt,cancellationToken);

            var data = await _sqlExecutionRepository.QueryAsync(sql);

            // Temporary Response
            return new GenerateSqlResponse
            {
                Success = true,
                Sql = sql,
                Data = data
            };
        }
    }
}
