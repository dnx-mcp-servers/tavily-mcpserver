using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Tavily.McpServer.ClientSdk.Models.Extract;

namespace Tavily.McpServer.ClientSdk.Models.Crawl;

/// <summary>
/// Predefined categories for filtering URLs during crawling.
/// </summary>
public enum Category
{
    /// <summary>
    /// Pages related to job opportunities and careers.
    /// </summary>
    Careers,
    /// <summary>
    /// Blog articles and posts.
    /// </summary>
    Blog,
    /// <summary>
    /// Documentation pages, manuals, and guides.
    /// </summary>
    Documentation,
    /// <summary>
    /// About pages describing the organization or product.
    /// </summary>
    About,
    /// <summary>
    /// Pricing information and plans.
    /// </summary>
    Pricing,
    /// <summary>
    /// Community forums, discussions, or user groups.
    /// </summary>
    Community,
    /// <summary>
    /// Developer resources, APIs, or SDKs.
    /// </summary>
    Developers,
    /// <summary>
    /// Contact information or contact forms.
    /// </summary>
    Contact,
    /// <summary>
    /// Media resources such as press releases or news.
    /// </summary>
    Media
}

/// <summary>
/// Request model for web crawling operation
/// </summary>
public record Request
{
    /// <summary>
    /// The root URL to begin the crawl
    /// </summary>
    [Description("The root URL to begin the crawl")]
    public required string Url { get; init; } 

    /// <summary>
    /// Max depth of the crawl. Defines how far from the base URL the crawler can explore.
    /// </summary>
    [Description("Max depth of the crawl. Defines how far from the base URL the crawler can explore, It must be at least 1")]
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
    public string[] SelectPaths { get; init; } = [];

    /// <summary>
    /// Regex patterns to select crawling to specific domains or subdomains (e.g., ^docs\.example\.com$)
    /// </summary>
    [Description("Regex patterns to select crawling to specific domains or subdomains (e.g., ^docs\\.example\\.com$)")]
    public string[] SelectDomains { get; init; } = [];

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

    /// <summary>
    /// Advanced extraction retrieves more data, including tables and embedded content, with higher success but may increase latency
    /// </summary>
    [Description("Advanced extraction retrieves more data, including tables and embedded content, with higher success but may increase latency")]
    public ExtractDepth ExtractDepth { get; init; } = ExtractDepth.Basic;

    /// <summary>
    /// The format of the extracted web page content. markdown returns content in markdown format. text returns plain text and may increase latency.
    /// </summary>
    [Description("The format of the extracted web page content. markdown returns content in markdown format. text returns plain text and may increase latency.")]
    public ContentFormat Format { get; init; } = ContentFormat.Markdown;

    /// <summary>
    /// Whether to include the favicon URL for each result
    /// </summary>
    [Description("Whether to include the favicon URL for each result")]
    public bool IncludeFavicon { get; init; } = false;
}
