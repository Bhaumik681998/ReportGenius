using ReportGenius.Application.Interfaces;
using ReportGenius.Application.Models.Schema;
using ReportGenius.Infrastructure.AI.Builders;
using ReportGenius.Infrastructure.AI.Clients;
using ReportGenius.Infrastructure.AI.Validators;

namespace ReportGenius.Infrastructure.AI
{
    public sealed class AIService : IAIService
    {
        private readonly IOllamaClient _ollamaClient;
        private readonly IDatabaseSchemaProvider _schemaProvider;
        private readonly IAITableSelector _tableSelector;

        public AIService(IOllamaClient ollamaClient, IDatabaseSchemaProvider schemaProvider, IAITableSelector tableSelector)
        {
            _ollamaClient = ollamaClient;
            _schemaProvider = schemaProvider;
            _tableSelector = tableSelector;
        }

        public async Task<string> GenerateSqlAsync(string userPrompt, CancellationToken cancellationToken = default)
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            // Read Database Schema
            var schema = await _schemaProvider.GetSchemaAsync(cancellationToken);

            Console.WriteLine("--------------------------------");
            Console.WriteLine($"Schema Read : {stopwatch.ElapsedMilliseconds} ms");
            Console.WriteLine($"Total Tables : {schema.Tables.Count}");
            Console.WriteLine("--------------------------------");

            // Step-1 : AI selects required tables
            var selectedTables = await _tableSelector.SelectTablesAsync(userPrompt, schema, cancellationToken);

            Console.WriteLine("--------------------------------");
            Console.WriteLine($"Table Selection : {stopwatch.ElapsedMilliseconds} ms");

            foreach (var table in selectedTables)
            {
                Console.WriteLine(table);
            }

            Console.WriteLine("--------------------------------");

            // Step-2 : Build filtered schema
            var filteredSchema = new DatabaseSchema();

            foreach (var table in schema.Tables)
            {
                if (selectedTables.Any(x => x.Equals(table.TableName, StringComparison.OrdinalIgnoreCase)))
                {
                    filteredSchema.Tables.Add(table);
                }
            }

            // Safety check
            if (!filteredSchema.Tables.Any())
            {
                throw new Exception("AI could not identify any matching table.");
            }

            // Step-3 : Build prompt using filtered schema only
            var prompt = PromptBuilder.BuildSqlPrompt(userPrompt, filteredSchema);

            Console.WriteLine("======================================");
            Console.WriteLine($"Prompt Length : {prompt.Length}");
            Console.WriteLine("======================================");

            // Generate SQL
            var sql = await _ollamaClient.GenerateAsync(prompt, cancellationToken);

            SqlValidator.Validate(sql);

            return sql;
        }

        public async Task<string> AskAsync(string prompt, CancellationToken cancellationToken = default)
        {
            var finalPrompt = PromptBuilder.BuildChatPrompt(prompt);

            return await _ollamaClient.GenerateAsync(finalPrompt, cancellationToken);
        }
    }
}