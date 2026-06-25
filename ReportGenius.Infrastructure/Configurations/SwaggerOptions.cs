namespace ReportGenius.Infrastructure.Configurations
{
    public sealed class SwaggerOptions
    {
        public const string SectionName = "Swagger";

        public bool Enable { get; set; } = true;

        public string Title { get; set; } = string.Empty;

        public string Version { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}
