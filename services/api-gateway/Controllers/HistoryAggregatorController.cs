// ═══════════════════════════════════════════════════════════════════
// HistoryAggregatorController.cs
// API Gateway — Unified History Aggregation
//
// This controller intercepts the /api/Quantity/history and
// /api/Quantity/count/{op} routes BEFORE YARP processes them.
// It fans out HTTP calls to all 3 downstream services, merges
// results, and returns a single unified response to the frontend.
// ═══════════════════════════════════════════════════════════════════

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ApiGateway.Controllers
{
    [ApiController]
    [Route("api/Quantity")]
    public class HistoryAggregatorController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;

        private static readonly JsonSerializerOptions _jsonOpts = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public HistoryAggregatorController(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        // ─── GET /api/Quantity/history — Unified history from all 3 services ───
        [Authorize]
        [HttpGet("history")]
        public async Task<IActionResult> GetAllHistory()
        {
            var token = ExtractToken();
            var tasks = new[]
            {
                FetchHistory("compare",    "/api/Quantity/history", token),
                FetchHistory("convert",    "/api/Quantity/history", token),
                FetchHistory("arithmetic", "/api/Quantity/history", token)
            };

            var results = await Task.WhenAll(tasks);
            var merged = results.SelectMany(r => r).OrderByDescending(x => x.CreatedAt).ToList();

            return Ok(new { success = true, data = merged, message = "Unified history fetched" });
        }

        // ─── GET /api/Quantity/history/operation/{operation} ───
        [Authorize]
        [HttpGet("history/operation/{operation}")]
        public async Task<IActionResult> GetByOperation(string operation)
        {
            var token = ExtractToken();

            // Route to the correct service based on operation type
            string serviceName = operation.ToLower() switch
            {
                "compare"  => "compare",
                "convert"  => "convert",
                "add" or "subtract" or "divide" => "arithmetic",
                _          => "compare"  // fallback
            };

            var items = await FetchHistory(serviceName, $"/api/Quantity/history/operation/{operation}", token);
            return Ok(new { success = true, data = items, message = $"History for '{operation}' fetched" });
        }

        // ─── GET /api/Quantity/count/{operation} ───
        [Authorize]
        [HttpGet("count/{operation}")]
        public async Task<IActionResult> GetCount(string operation)
        {
            var token = ExtractToken();

            string serviceName = operation.ToLower() switch
            {
                "compare"  => "compare",
                "convert"  => "convert",
                "add" or "subtract" or "divide" => "arithmetic",
                // For "all" aggregated count, sum from all services
                _ => "all"
            };

            if (serviceName == "all")
            {
                // Sum counts from all 3 services
                var counts = await Task.WhenAll(
                    FetchCount("compare",    $"/api/Quantity/count/{operation}", token),
                    FetchCount("convert",    $"/api/Quantity/count/{operation}", token),
                    FetchCount("arithmetic", $"/api/Quantity/count/{operation}", token)
                );
                return Ok(new { success = true, data = counts.Sum(), message = $"Count for '{operation}'" });
            }

            var count = await FetchCount(serviceName, $"/api/Quantity/count/{operation}", token);
            return Ok(new { success = true, data = count, message = $"Count for '{operation}' fetched" });
        }

        // ─── Private Helpers ───────────────────────────────────────────────────

        private async Task<List<HistoryItem>> FetchHistory(string clientName, string path, string? token)
        {
            try
            {
                var client = CreateClient(clientName, token);
                var response = await client.GetAsync(path);

                if (!response.IsSuccessStatusCode) return new List<HistoryItem>();

                var json = await response.Content.ReadAsStringAsync();
                var wrapper = JsonSerializer.Deserialize<ApiResponse<List<HistoryItem>>>(json, _jsonOpts);
                return wrapper?.Data ?? new List<HistoryItem>();
            }
            catch
            {
                return new List<HistoryItem>();
            }
        }

        private async Task<int> FetchCount(string clientName, string path, string? token)
        {
            try
            {
                var client = CreateClient(clientName, token);
                var response = await client.GetAsync(path);

                if (!response.IsSuccessStatusCode) return 0;

                var json = await response.Content.ReadAsStringAsync();
                var wrapper = JsonSerializer.Deserialize<ApiResponse<int>>(json, _jsonOpts);
                return wrapper?.Data ?? 0;
            }
            catch
            {
                return 0;
            }
        }

        private HttpClient CreateClient(string clientName, string? token)
        {
            var client = _httpClientFactory.CreateClient(clientName);
            if (!string.IsNullOrEmpty(token))
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return client;
        }

        private string? ExtractToken()
        {
            var authHeader = Request.Headers["Authorization"].FirstOrDefault();
            if (authHeader != null && authHeader.StartsWith("Bearer "))
                return authHeader["Bearer ".Length..].Trim();
            return null;
        }
    }

    // ─── Internal DTOs for deserializing downstream responses ───
    public class HistoryItem
    {
        public int Id { get; set; }
        public string OperationType { get; set; } = string.Empty;
        public int OperationCode { get; set; }
        public string InputType { get; set; } = string.Empty;
        public string OutputType { get; set; } = string.Empty;
        public string InputData { get; set; } = string.Empty;
        public string ResultData { get; set; } = string.Empty;
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }

    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
