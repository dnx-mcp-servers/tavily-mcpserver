using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using UsageResponse = Tavily.McpServer.ClientSdk.Models.Usage.Response;
using SearchRequest = Tavily.McpServer.ClientSdk.Models.Search.Request;
using SearchResponse = Tavily.McpServer.ClientSdk.Models.Search.Response;
using ExtractRequest = Tavily.McpServer.ClientSdk.Models.Extract.Request;
using ExtractResponse = Tavily.McpServer.ClientSdk.Models.Extract.Response;
using CrawlRequest = Tavily.McpServer.ClientSdk.Models.Crawl.Request;
using CrawlResponse = Tavily.McpServer.ClientSdk.Models.Crawl.Response;
using MapRequest = Tavily.McpServer.ClientSdk.Models.Map.Request;
using MapResponse = Tavily.McpServer.ClientSdk.Models.Map.Response;


namespace Tavily.McpServer.ClientSdk;

/// <summary>
/// Client for interacting with the Tavily Server API.
/// Provides methods for usage, search, extract, crawl, and map operations.
/// </summary>
public class TavilyClient(HttpClient client)
{
    private readonly JsonSerializerOptions jsonSerializer = new()
    {
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        WriteIndented = true
    };

    /// <summary>
    /// Gets the current API usage statistics.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The usage response, or null if not available.</returns>
    public async ValueTask<UsageResponse?> GetUsageAsync(CancellationToken cancellationToken = default)
    {
        var response = await client.GetAsync("usage", cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<UsageResponse>(jsonSerializer, cancellationToken);
    }

    /// <summary>
    /// Performs a search operation using the specified request.
    /// </summary>
    /// <param name="request">The search request parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The search response, or null if not available.</returns>
    public async Task<SearchResponse?> SearchAsync(SearchRequest request, CancellationToken cancellationToken = default)
    {
        var response = await client.PostAsJsonAsync("search", request, jsonSerializer, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<SearchResponse>(jsonSerializer, cancellationToken);
    }

    /// <summary>
    /// Extracts information using the specified extract request.
    /// </summary>
    /// <param name="request">The extract request parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The extract response, or null if not available.</returns>
    public async Task<ExtractResponse?> ExtractAsync(ExtractRequest request, CancellationToken cancellationToken = default)
    {
        var response = await client.PostAsJsonAsync("extract", request, jsonSerializer, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ExtractResponse>(jsonSerializer, cancellationToken);
    }

    /// <summary>
    /// Crawls content using the specified crawl request.
    /// </summary>
    /// <param name="request">The crawl request parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The crawl response, or null if not available.</returns>
    public async Task<CrawlResponse?> CrawlAsync(CrawlRequest request, CancellationToken cancellationToken = default)
    {
        var response = await client.PostAsJsonAsync("crawl", request, jsonSerializer, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<CrawlResponse>(jsonSerializer, cancellationToken);
    }

    /// <summary>
    /// Maps content using the specified map request.
    /// </summary>
    /// <param name="request">The map request parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The map response, or null if not available.</returns>
    public async Task<MapResponse?> MapAsync(MapRequest request, CancellationToken cancellationToken = default)
    {
        var response = await client.PostAsJsonAsync("map", request, jsonSerializer, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<MapResponse>(jsonSerializer, cancellationToken);
    }
}
