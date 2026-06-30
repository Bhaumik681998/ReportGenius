using System.Text.Json.Serialization;

namespace ReportGenius.Infrastructure.AI.Models
{
    public sealed class OllamaRequest
    {
        public string Model { get; set; } = string.Empty;

        public string Prompt { get; set; } = string.Empty;

        public bool Stream { get; set; } = false;

        public OllamaOptions Options { get; set; } = new();
        public string KeepAlive { get; set; } = "30m";
    }

    public sealed class OllamaOptions
    {
        public double Temperature { get; set; }
        [JsonPropertyName("num_predict")]
        public int NumPredict { get; set; }
        public int NumCtx { get; set; } = 2048;
    }
}
