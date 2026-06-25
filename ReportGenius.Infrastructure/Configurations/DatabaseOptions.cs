namespace ReportGenius.Infrastructure.Configurations
{
    public sealed class DatabaseOptions
    {
        public const string SectionName = "Database";

        public string Provider { get; set; } = string.Empty;

        public int CommandTimeout { get; set; } = 30;

        public bool EnableRetry { get; set; } = true;

        public int MaxRetryCount { get; set; } = 3;
    }
}
