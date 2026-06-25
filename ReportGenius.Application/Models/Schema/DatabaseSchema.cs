namespace ReportGenius.Application.Models.Schema
{
    public sealed class DatabaseSchema
    {
        public List<TableSchema> Tables { get; set; } = new();
    }
}
