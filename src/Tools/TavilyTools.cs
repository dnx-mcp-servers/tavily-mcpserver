using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using System.ComponentModel;
using Tavily.McpServer.ClientSdk;
using CrawlRequest = Tavily.McpServer.ClientSdk.Models.Crawl.Request;
using ExtractRequest = Tavily.McpServer.ClientSdk.Models.Extract.Request;
using MapRequest = Tavily.McpServer.ClientSdk.Models.Map.Request;
using SearchRequest = Tavily.McpServer.ClientSdk.Models.Search.Request;

namespace Tavily.McpServer.Tools;

/// <summary>
/// Provides tools for web search, extraction, crawling, and mapping using the Tavily API.
/// </summary>
/// <remarks>
/// Each method is exposed as an MCP server tool for integration with ModelContextProtocol.
/// </remarks>
public class TavilyTools(TavilyClient tavilyClient)
{
    /// <summary>
    /// Performs a powerful web search using Tavily's AI search engine and returns comprehensive, real-time results.
    /// </summary>
    /// <param name="searchRequest">The search request parameters.</param>
    /// <param name="ct">A cancellation token.</param>
    /// <returns>A <see cref="TextContentBlock"/> containing the search results and details.</returns>
    [McpServerTool(Name = "tavily-search")]
    [Description("A powerful web search tool that provides comprehensive, real-time results using Tavily's AI search engine. Returns relevant web content with customizable parameters for result count, content type, and domain filtering. Ideal for gathering current information, news, and detailed web content analysis.")]
    public async Task<TextContentBlock> TavilySearch(SearchRequest searchRequest, CancellationToken ct)
    {
        var response = await tavilyClient.SearchAsync(searchRequest, ct);

        var output = new List<string>();

        if (!string.IsNullOrEmpty(response!.Answer))
        {
            output.Add($"Answer: {response.Answer}");
        }

        output.Add("Detailed Results:");
        response.Results.ForEach(result =>
        {
            output.Add($"\nTitle: {result.Title}");
            output.Add($"URL: {result.Url}");
            output.Add($"Content:{result.Content}");
            if (!string.IsNullOrEmpty(result.RawContent))
            {
                output.Add($"Raw Content: {result.RawContent}");
            }
            if (!string.IsNullOrEmpty(result.Favicon))
            {
                output.Add($"Favicon: {result.Favicon}");
            }
        });

        // Add images section if available
        if (response.Images.Count > 0)
        {
            int numberIndex = 0;
            output.Add("\nImages:");
            response.Images.ForEach((image) =>
            {
                numberIndex++;
                output.Add($"\n[{numberIndex}] URL: {image.Url}");
                if (!string.IsNullOrEmpty(image.Description))
                {
                    output.Add($"   Description: {image.Description}");
                }
            });
        }
        return new TextContentBlock { Text = string.Join('\n', output) };
    }

    /// <summary>
    /// Extracts and processes raw web content from specified URLs for data collection and analysis.
    /// </summary>
    /// <param name="extractRequest">The extraction request parameters.</param>
    /// <param name="ct">A cancellation token.</param>
    /// <returns>A <see cref="TextContentBlock"/> containing the extracted content and details.</returns>
    [McpServerTool(Name = "tavily-extract")]
    [Description("A powerful web content extraction tool that retrieves and processes raw content from specified URLs, ideal for data collection, content analysis, and research tasks.")]
    public async Task<TextContentBlock> TavilyExtract(ExtractRequest extractRequest, CancellationToken ct)
    {
        var response = await tavilyClient.ExtractAsync(extractRequest, ct);

        var output = new List<string>
        {
            "Detailed Results:"
        };
        response!.Results.ForEach(result =>
        {
            output.Add($"URL: {result.Url}");
            if (!string.IsNullOrEmpty(result.RawContent))
            {
                output.Add($"Raw Content: {result.RawContent}");
            }
            if (!string.IsNullOrEmpty(result.Favicon))
            {
                output.Add($"Favicon: {result.Favicon}");
            }

            // Add images section if available
            if (result.Images.Count > 0)
            {
                int numberIndex = 0;
                output.Add("\nImages:");
                result.Images.ForEach((image) =>
                {
                    numberIndex++;
                    output.Add($"\n[{numberIndex}] URL: {image}");
                });
            }
        });

        return new TextContentBlock { Text = string.Join('\n', output) };
    }

    /// <summary>
    /// Initiates a structured web crawl from a base URL, following internal links and expanding like a tree.
    /// </summary>
    /// <param name="crawlRequest">The crawl request parameters.</param>
    /// <param name="ct">A cancellation token.</param>
    /// <returns>A <see cref="TextContentBlock"/> containing the crawl results and details.</returns>
    [McpServerTool(Name = "tavily-crawl")]
    [Description("A powerful web crawler that initiates a structured web crawl starting from a specified base URL. The crawler expands from that point like a tree, following internal links across pages. You can control how deep and wide it goes, and guide it to focus on specific sections of the site.")]
    public async Task<TextContentBlock> TavilyCrawl(CrawlRequest crawlRequest, CancellationToken ct)
    {
        var response = await tavilyClient.CrawlAsync(crawlRequest, ct);

        var output = new List<string>
        {
            "Crawl Results:",
            $"Base URL: {response!.BaseUrl}",
            "\nCrawled Pages:"
        };
        int numberIndex = 0;
        response.Results.ForEach((page) =>
        {
            numberIndex++;

            output.Add($"\n[{numberIndex}] URL: {page.Url}");
            if (!string.IsNullOrEmpty(page.RawContent))
            {
                var contentPreview = page.RawContent.Length > 200
                  ? string.Concat(page.RawContent.AsSpan(0, 200), "...")
                  : page.RawContent;
                output.Add($"Content: {contentPreview}");
            }
            if (!string.IsNullOrEmpty(page.Favicon))
            {
                output.Add($"Favicon: {page.Favicon}");
            }
        });

        return new TextContentBlock { Text = string.Join('\n', output) };
    }

    /// <summary>
    /// Creates a structured map of website URLs to analyze site structure and navigation paths.
    /// </summary>
    /// <param name="mapRequest">The map request parameters.</param>
    /// <param name="ct">A cancellation token.</param>
    /// <returns>A <see cref="TextContentBlock"/> containing the site map results and details.</returns>
    [McpServerTool(Name = "tavily-map")]
    [Description("A powerful web mapping tool that creates a structured map of website URLs, allowing you to discover and analyze site structure, content organization, and navigation paths. Perfect for site audits, content discovery, and understanding website architecture.")]
    public async Task<TextContentBlock> TavilyMap(MapRequest mapRequest, CancellationToken ct)
    {
        var response = await tavilyClient.MapAsync(mapRequest, ct);

        var output = new List<string>
        {
            "Site Map Results:",
            $"Base URL: {response!.BaseUrl}",
            "\nMapped Pages:"
        };
        int numberIndex = 0;
        response.Results.ForEach((page) =>
        {
            numberIndex++;
            output.Add($"\n[{numberIndex}] URL: {page}");
        });
        return new TextContentBlock { Text = string.Join('\n', output) };
    }
}
