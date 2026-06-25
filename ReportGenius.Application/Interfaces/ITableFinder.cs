using ReportGenius.Application.Models.Schema;

namespace ReportGenius.Application.Interfaces
{
    public interface ITableFinder
    {
        IReadOnlyList<TableSchema> FindRelevantTables(string userPrompt,DatabaseSchema schema);
    }
}
