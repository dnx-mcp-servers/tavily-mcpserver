using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Tavily.McpServer.ClientSdk.Models.Crawl;

namespace Tavily.McpServer.ClientSdk.Models.Map;

/// <summary>
/// Request model for web mapping operation
/// </summary>
public record Request
{
    /// <summary>
    /// The root URL to begin the mapping
    /// </summary>
    [Required]
    [Description("The root URL to begin the mapping")]
    public string Url { get; init; } = string.Empty;

    /// <summary>
    /// Max depth of the mapping. Defines how far from the base URL the crawler can explore
    /// </summary>
    [Description("Max depth of the mapping. Defines how far from the base URL the crawler can explore, It must be at least 1")]
    [Range(1, int.MaxValue, ErrorMessage = "Max depth must be at least 1")]
    public int MaxDepth { get; init; } = 1;

    /// <summary>
    /// Max number of links to follow per level of the tree (i.e., per page)
    /// </summary>
    [Description("Max number of links to follow per level of the tree (i.e., per page), It must be at least 1")]
    [Range(1, int.MaxValue, ErrorMessage = "Max breadth must be at least 1")]
    public int MaxBreadth { get; init; } = 20;

    /// <summary>
    /// Total number of links the crawler will process before stopping
    /// </summary>
    [Description("Total number of links the crawler will process before stopping, It must be at least 1")]
    [Range(1, int.MaxValue, ErrorMessage = "Limit must be at least 1")]
    public int Limit { get; init; } = 50;

    /// <summary>
    /// Natural language instructions for the crawler
    /// </summary>
    [Description("Natural language instructions for the crawler")]
    public string? Instructions { get; init; }

    /// <summary>
    /// Regex patterns to select only URLs with specific path patterns (e.g., /docs/.*, /api/v1.*)
    /// </summary>
    [Description("Regex patterns to select only URLs with specific path patterns (e.g., /docs/.*, /api/v1.*)")]
    public string[] SelectPaths { get; init; } = Array.Empty<string>();

    /// <summary>
    /// Regex patterns to select crawling to specific domains or subdomains (e.g., ^docs\.example\.com$)
    /// </summary>
    [Description("Regex patterns to select crawling to specific domains or subdomains (e.g., ^docs\\.example\\.com$)")]
    public string[] SelectDomains { get; init; } = Array.Empty<string>();

    /// <summary>
    /// Whether to allow following links that go to external domains
    /// </summary>
    [Description("Whether to allow following links that go to external domains")]
    public bool AllowExternal { get; init; } = false;

    /// <summary>
    /// Filter URLs using predefined categories like documentation, blog, api, etc
    /// </summary>
    [Description("Filter URLs using predefined categories like Careers, Blog, Documentation, About, Pricing, Community, Developers, Contact, Media")]
    public Category[] Categories { get; init; } = [];
}
