namespace ReportGenius.Infrastructure.AI.Metadata
{
    public sealed class TableMetadata
    {
        public string TableName { get; set; } = string.Empty;

        public List<string> Columns { get; set; } = new();
    }
}
