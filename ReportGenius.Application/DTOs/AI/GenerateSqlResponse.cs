namespace ReportGenius.Application.DTOs.AI
{
    public sealed class GenerateSqlResponse
    {
        public bool Success { get; set; }

        public string Sql { get; set; } = string.Empty;
    }
}
