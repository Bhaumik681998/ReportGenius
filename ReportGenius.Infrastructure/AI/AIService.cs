using ReportGenius.Application.Interfaces;
using ReportGenius.Infrastructure.AI.Builders;
using ReportGenius.Infrastructure.AI.Clients;
using ReportGenius.Infrastructure.AI.Validators;

namespace ReportGenius.Infrastructure.AI
{
    public sealed class AIService : IAIService
    {
        private readonly IOllamaClient _ollamaClient;
        private readonly IDatabaseSchemaProvider _schemaProvider;

        public AIService(IOllamaClient ollamaClient,IDatabaseSchemaProvider schemaProvider)
        {
            _ollamaClient = ollamaClient;
            _schemaProvider = schemaProvider;
        }

        public async Task<string> GenerateSqlAsync(string userPrompt,CancellationToken cancellationToken = default)
        {
            // Read Database Schema
            var schema = await _schemaProvider.GetSchemaAsync(cancellationToken);

            Console.WriteLine("========== SCHEMA ==========");

            foreach (var table in schema.Tables.Take(5))
            {
                Console.WriteLine($"TABLE = [{table.TableName}]");

                foreach (var column in table.Columns.Take(5))
                {
                    Console.WriteLine($"   COLUMN = [{column.ColumnName}] ({column.DataType})");
                }
            }

            Console.WriteLine("============================");

            // Build Enterprise Prompt
            var prompt = PromptBuilder.BuildSqlPrompt(userPrompt,schema);

            Console.WriteLine("======================================");
            Console.WriteLine($"Prompt Length : {prompt.Length}");
            Console.WriteLine("======================================");

            // Generate SQL
            var sql = await _ollamaClient.GenerateAsync(prompt,cancellationToken);

            SqlValidator.Validate(sql);

            return sql;
        }

        public async Task<string> AskAsync(string prompt,CancellationToken cancellationToken = default)
        {
            var finalPrompt = PromptBuilder.BuildChatPrompt(prompt);

            return await _ollamaClient.GenerateAsync(finalPrompt,cancellationToken);
        }
    }
}