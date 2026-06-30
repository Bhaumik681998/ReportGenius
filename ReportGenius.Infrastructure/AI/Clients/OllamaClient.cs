using Microsoft.Extensions.Options;
using ReportGenius.Infrastructure.AI.Models;
using ReportGenius.Infrastructure.Configurations;
using System.Diagnostics;
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

        //public async Task<string> GenerateAsync(string prompt,CancellationToken cancellationToken = default)
        //{
        //    var totalWatch = Stopwatch.StartNew();
        //    var watch = new Stopwatch();

        //    //====================================================
        //    // Build Request
        //    //====================================================

        //    watch.Start();

        //    var request = new OllamaRequest
        //    {
        //        Model = _aiOptions.Model!,
        //        Prompt = prompt,
        //        Stream = false,
        //        KeepAlive = "30m",
        //        Options = new OllamaOptions
        //        {
        //            Temperature = _aiOptions.Temperature,
        //            NumPredict = _aiOptions.MaxTokens,
        //            NumCtx = 2048
        //        }
        //    };

        //    watch.Stop();

        //    Console.WriteLine();
        //    Console.WriteLine("====================================================");
        //    Console.WriteLine("                OLLAMA REQUEST");
        //    Console.WriteLine("====================================================");
        //    Console.WriteLine($"Model          : {request.Model}");
        //    Console.WriteLine($"Prompt Length  : {request.Prompt.Length}");
        //    Console.WriteLine($"Temperature    : {request.Options.Temperature}");
        //    Console.WriteLine($"Max Tokens     : {request.Options.NumPredict}");
        //    Console.WriteLine($"Context Size   : {request.Options.NumCtx}");
        //    Console.WriteLine($"Build Request  : {watch.ElapsedMilliseconds} ms");
        //    Console.WriteLine("====================================================");

        //    //====================================================
        //    // HTTP Request
        //    //====================================================

        //    watch.Restart();

        //    var response = await _httpClient.PostAsJsonAsync(
        //        "/api/generate",
        //        request,
        //        cancellationToken);

        //    watch.Stop();

        //    Console.WriteLine();
        //    Console.WriteLine("=============== HTTP REQUEST =================");
        //    Console.WriteLine($"Status Code    : {(int)response.StatusCode}");
        //    Console.WriteLine($"HTTP Time      : {watch.ElapsedMilliseconds} ms");
        //    Console.WriteLine("==============================================");

        //    response.EnsureSuccessStatusCode();

        //    //====================================================
        //    // Deserialize Response
        //    //====================================================

        //    watch.Restart();

        //    var result = await response.Content.ReadFromJsonAsync<OllamaResponse>(
        //        cancellationToken: cancellationToken);

        //    watch.Stop();

        //    Console.WriteLine();
        //    Console.WriteLine("============= RESPONSE READ ===================");
        //    Console.WriteLine($"Deserialize    : {watch.ElapsedMilliseconds} ms");
        //    Console.WriteLine("===============================================");

        //    if (result == null)
        //        throw new InvalidOperationException("Invalid response received from Ollama.");

        //    totalWatch.Stop();

        //    Console.WriteLine();
        //    Console.WriteLine("=============== TOTAL OLLAMA ==================");
        //    Console.WriteLine($"Total Time     : {totalWatch.ElapsedMilliseconds} ms");
        //    Console.WriteLine("===============================================");

        //    Console.WriteLine();
        //    Console.WriteLine("============== OLLAMA METRICS ==============");

        //    Console.WriteLine($"Total Duration        : {result.TotalDuration / 1_000_000} ms");

        //    Console.WriteLine($"Model Load            : {result.LoadDuration / 1_000_000} ms");

        //    Console.WriteLine($"Prompt Tokens         : {result.PromptEvalCount}");

        //    Console.WriteLine($"Prompt Eval           : {result.PromptEvalDuration / 1_000_000} ms");

        //    Console.WriteLine($"Generated Tokens      : {result.EvalCount}");

        //    Console.WriteLine($"Generation Time       : {result.EvalDuration / 1_000_000} ms");

        //    Console.WriteLine("============================================");

        //    return result.Response.Trim();
        //}

        public async Task<string> GenerateAsync(string prompt, CancellationToken cancellationToken = default)
        {
            var request = new OllamaRequest
            {
                Model = _aiOptions.Model!,
                Prompt = prompt,
                Stream = false,
                KeepAlive = "30m",
                Options = new OllamaOptions
                {
                    Temperature = _aiOptions.Temperature,
                    NumPredict = _aiOptions.MaxTokens,
                    NumCtx = 2048
                }
            };

            Console.WriteLine("========= OLLAMA REQUEST =========");
            Console.WriteLine($"Model          : {request.Model}");
            Console.WriteLine($"Prompt Length  : {request.Prompt.Length}");
            Console.WriteLine($"Temperature    : {request.Options.Temperature}");
            Console.WriteLine($"Max Tokens     : {request.Options.NumPredict}");
            Console.WriteLine($"Context Size   : {request.Options.NumCtx}");
            Console.WriteLine("==================================");

            var response = await _httpClient.PostAsJsonAsync("/api/generate", request, cancellationToken);

            Console.WriteLine("Request Sent Successfully");

            response.EnsureSuccessStatusCode();

            Console.WriteLine(response.StatusCode);

            var result = await response.Content.ReadFromJsonAsync<OllamaResponse>(cancellationToken: cancellationToken);

            if (result == null)
                throw new InvalidOperationException("Invalid response received from Ollama.");

            return result.Response.Trim();
        }
    }
}
