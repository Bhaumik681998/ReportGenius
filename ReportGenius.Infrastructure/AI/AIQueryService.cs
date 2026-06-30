using ReportGenius.Application.DTOs.AI;
using ReportGenius.Application.Interfaces;
using System.Diagnostics;

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
            var stopwatch = Stopwatch.StartNew();

            // Step-1 : Generate SQL using AI
            var sql = await _aiService.GenerateSqlAsync(request.Prompt,cancellationToken);

            stopwatch.Stop();

            Console.WriteLine("--------------------------------");
            Console.WriteLine($"SQL Generation : {stopwatch.ElapsedMilliseconds} ms");
            Console.WriteLine("--------------------------------");

            stopwatch.Restart();

            var data = await _sqlExecutionRepository.QueryAsync(sql);

            stopwatch.Stop();

            Console.WriteLine("--------------------------------");
            Console.WriteLine($"SQL Execution : {stopwatch.ElapsedMilliseconds} ms");
            Console.WriteLine("--------------------------------");

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
