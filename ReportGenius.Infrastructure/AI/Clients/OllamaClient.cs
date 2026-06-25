using Microsoft.Extensions.Options;
using ReportGenius.Infrastructure.AI.Models;
using ReportGenius.Infrastructure.Configurations;
using System.Net.Http.Json;

namespace ReportGenius.Infrastructure.AI.Clients
{
    public sealed class OllamaClient : IOllamaClient
    {
        private readonly HttpClient _httpClient;
        private readonly AIOptions _aiOptions;

        public OllamaClient(HttpClient httpClient,IOptions<AIOptions> aiOptions)
        {
            _httpClient = httpClient;
            _aiOptions = aiOptions.Value;

            _httpClient.BaseAddress = new Uri(_aiOptions.BaseUrl!);
            _httpClient.Timeout = TimeSpan.FromSeconds(_aiOptions.TimeoutSeconds);
        }

        public async Task<string> GenerateAsync(string prompt,CancellationToken cancellationToken = default)
        {
            var request = new OllamaRequest
            {
                Model = _aiOptions.Model!,
                Prompt = prompt,
                Stream = false,
                Options = new OllamaOptions
                {
                    Temperature = _aiOptions.Temperature,
                    NumPredict = _aiOptions.MaxTokens
                }
            };

            var response = await _httpClient.PostAsJsonAsync("/api/generate",request,cancellationToken);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<OllamaResponse>(cancellationToken: cancellationToken);

            if (result == null)
                throw new InvalidOperationException("Invalid response received from Ollama.");

            return result.Response.Trim();
        }
    }
}
