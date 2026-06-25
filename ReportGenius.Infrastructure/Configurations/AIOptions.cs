namespace ReportGenius.Infrastructure.Configurations
{
    public sealed class AIOptions
    {
        public const string SectionName = "AI";

        public string Provider { get; set; } = string.Empty;

        public string BaseUrl { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public double Temperature { get; set; } = 0.3;

        public int MaxTokens { get; set; } = 4096;

        public int TimeoutSeconds { get; set; } = 120;

        public bool EnableLogging { get; set; } = true;
    }
}
