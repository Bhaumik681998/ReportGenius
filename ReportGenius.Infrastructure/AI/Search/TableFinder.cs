using ReportGenius.Application.Interfaces;
using ReportGenius.Application.Models.Schema;

namespace ReportGenius.Infrastructure.AI.Search
{
    public sealed class TableFinder : ITableFinder
    {
        public IReadOnlyList<TableSchema> FindRelevantTables(string userPrompt,DatabaseSchema schema)
        {
            return new List<TableSchema>();
        }
    }
}
