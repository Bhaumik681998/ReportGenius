using ReportGenius.Application.Models.Schema;
using System.Text;

namespace ReportGenius.Infrastructure.AI.Builders
{
    public static class PromptBuilder
    {
        public static string BuildSqlPrompt(string userPrompt, DatabaseSchema schema)
        {
            var sb = new StringBuilder();

            sb.AppendLine("""
You are an expert PostgreSQL SQL Generator.

Return ONLY SQL.

Rules

1. Return ONLY SQL.
2. No explanation.
3. No markdown.
4. Use ONLY tables listed below.
5. Use ONLY columns listed below.
6. Never use SELECT *.
7. Always select only required columns.
8. Never generate DELETE.
9. Never generate DROP.
10. Never generate ALTER.

Database Schema

""");

            foreach (var table in schema.Tables)
            {
                sb.AppendLine($"Table : {table.TableName}");

                sb.Append("Columns : ");

                sb.AppendLine(string.Join(",",
                    table.Columns.Select(x => x.ColumnName)));

                sb.AppendLine();
            }

            sb.AppendLine("User Request");

            sb.AppendLine(userPrompt);

            sb.AppendLine();

            sb.Append("SQL : ");

            return sb.ToString();
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
