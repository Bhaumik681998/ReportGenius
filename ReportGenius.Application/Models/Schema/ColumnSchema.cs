namespace ReportGenius.Application.Models.Schema
{
    public sealed class ColumnSchema
    {
        public string ColumnName { get; set; } = string.Empty;

        public string DataType { get; set; } = string.Empty;

        public bool IsNullable { get; set; }

        public int OrdinalPosition { get; set; }
    }
}
