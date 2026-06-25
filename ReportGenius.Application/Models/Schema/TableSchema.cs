namespace ReportGenius.Application.Models.Schema
{
    public sealed class TableSchema
    {
        public string TableName { get; set; } = string.Empty;

        public List<ColumnSchema> Columns { get; set; } = new();
    }
}
