using ReportGenius.Application.Models.Schema;
using System.Text;

namespace ReportGenius.Infrastructure.AI.Builders
{
    public static class PromptBuilder
    {
        public static string BuildSqlPrompt(string userPrompt,DatabaseSchema schema)
        {
            var schemaBuilder = new StringBuilder();

            foreach (var table in schema.Tables)
            {
                schemaBuilder.AppendLine($"Table : {table.TableName}");

                foreach (var column in table.Columns)
                {
                    schemaBuilder.AppendLine(
                        $"   - {column.ColumnName} ({column.DataType})");
                }

                schemaBuilder.AppendLine();
            }

            return $"""
            You are an expert PostgreSQL SQL Generator.
            
            Below is the database schema.
            
            {schemaBuilder}
            
            Rules
            
            1. Return ONLY SQL.
            2. Never explain.
            3. Never return markdown.
            4. Never use tables that are not present in the schema.
            5. Never use columns that are not present in the schema.
            6. Never generate DROP.
            7. Never generate TRUNCATE.
            8. Never generate ALTER.
            9. Never generate CREATE DATABASE.
            10. Never generate DELETE without WHERE.
            
            User Request
            
            {userPrompt}
            
            SQL:
            """;
        }

        public static string BuildChatPrompt(string prompt)
        {
            return $"""
            You are ReportGenius AI Assistant.
            
            Answer professionally.
            
            User:
            
            {prompt}
            """;
        }
    }
}
