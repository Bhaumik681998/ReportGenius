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

        //public async Task<IReadOnlyList<string>> SelectTablesAsync(string userPrompt, DatabaseSchema schema, CancellationToken cancellationToken = default)
        //{
        //    var prompt = TableSelectionPromptBuilder.Build(userPrompt, schema);

        //    Console.WriteLine(prompt.Length);

        //    var response = await _ollamaClient.GenerateAsync(prompt, cancellationToken);

        //    var tables = response.Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).Distinct().ToList();

        //    return tables;
        //}

        //        public async Task<IReadOnlyList<string>> SelectTablesAsync(string userPrompt, DatabaseSchema schema, CancellationToken cancellationToken = default)
        //        {
        //            var prompt = TableSelectionPromptBuilder.Build(userPrompt, schema);

        //            Console.WriteLine(prompt.Length);
        //            var response = await _ollamaClient.GenerateAsync(
        //                """
        //Return ONLY the table name.

        //User Request:
        //Show all users

        //Available Tables:
        //usermaster
        //categorymaster
        //citymaster

        //Answer:
        //""",
        //                cancellationToken);

        //            //var tables = response.Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).Distinct().ToList();
        //            var tables = response
        //    .Split('\n', StringSplitOptions.RemoveEmptyEntries)
        //    .Select(x => x.Trim())
        //    .Select(x => x.Replace("*", ""))
        //    .Select(x => x.Replace("`", ""))
        //    .Select(x => x.Replace("-", ""))
        //    .Select(x => x.Trim())
        //    .Distinct(StringComparer.OrdinalIgnoreCase)
        //    .ToList();

        //            return tables;
        //        }

        public async Task<IReadOnlyList<string>> SelectTablesAsync(string userPrompt, DatabaseSchema schema, CancellationToken cancellationToken = default)
        {
            var prompt = TableSelectionPromptBuilder.Build(userPrompt, schema);

            Console.WriteLine("========== TABLE SELECTION ==========");
            Console.WriteLine(prompt);
            Console.WriteLine("=====================================");

            var response = await _ollamaClient.GenerateAsync(
                prompt,
                cancellationToken);

            var tables = response
                .Split('\n', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .Select(x => x.Replace("*", ""))
                .Select(x => x.Replace("`", ""))
                .Select(x => x.Replace("-", ""))
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            return tables;
        }

    }
}
