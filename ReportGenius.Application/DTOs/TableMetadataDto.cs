namespace ReportGenius.Application.DTOs
{
    public sealed class TableMetadataDto
    {
        public string TableName { get; set; } = string.Empty;

        public IReadOnlyList<string> Columns { get; set; } = Array.Empty<string>();
    }
}
