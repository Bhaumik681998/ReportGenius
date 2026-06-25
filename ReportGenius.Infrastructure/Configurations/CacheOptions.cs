namespace ReportGenius.Infrastructure.Configurations
{
    public sealed class CacheOptions
    {
        public const string SectionName = "Cache";

        public bool EnableCaching { get; set; } = true;

        public int DefaultDurationInMinutes { get; set; } = 30;
    }
}
