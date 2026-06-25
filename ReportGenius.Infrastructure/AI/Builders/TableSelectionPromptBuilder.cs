using ReportGenius.Application.Models.Schema;
using System.Text;

namespace ReportGenius.Infrastructure.AI.Builders
{
    public static class TableSelectionPromptBuilder
    {
        public static string Build(string userPrompt,DatabaseSchema schema)
        {
            var sb = new StringBuilder();

            sb.AppendLine("""
            You are an expert Database Architect.
            
            Your job is to identify ONLY the relevant database tables.
            
            Rules
            
            1. Return ONLY table names.
            2. One table name per line.
            3. Never explain.
            4. Never generate SQL.
            5. Never generate markdown.
            6. If no table matches return NONE.
            
            Available Tables
            
            """);

            foreach (var table in schema.Tables)
            {
                sb.AppendLine(table.TableName);
            }

            sb.AppendLine();

            sb.AppendLine("User Request");

            sb.AppendLine(userPrompt);

            sb.AppendLine();

            sb.AppendLine("Relevant Tables");

            return sb.ToString();
        }
    }
}
