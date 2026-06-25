using ReportGenius.Application.Models.Schema;
using ReportGenius.Infrastructure.AI.Builders;

namespace ReportGenius.Infrastructure.AI.Clients
{
    public sealed class AITableSelector : IAITableSelector
    {
        private readonly IOllamaClient _ollamaClient;

        public AITableSelector(IOllamaClient ollamaClient)
        {
            _ollamaClient = ollamaClient;
        }

        public async Task<IReadOnlyList<string>> SelectTablesAsync(string userPrompt, DatabaseSchema schema, CancellationToken cancellationToken = default)
        {
            var prompt = TableSelectionPromptBuilder.Build(userPrompt, schema);

            var response = await _ollamaClient.GenerateAsync(prompt, cancellationToken);

            var tables = response.Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).Distinct().ToList();

            return tables;
        }
    }
}
