namespace ReportGenius.Infrastructure.Configurations
{
    public sealed class FileUploadOptions
    {
        public const string SectionName = "FileUpload";

        public int MaxFileSizeInMB { get; set; }

        public List<string> AllowedExtensions { get; set; } = new();
    }
}
