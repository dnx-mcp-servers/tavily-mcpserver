namespace Tavily.McpServer.ClientSdk.Models.Crawl;

/// <summary>
/// Represents a single crawled web page result.
/// </summary>
public class Result
{
    /// <summary>
    /// The URL of the crawled web page.
    /// </summary>
    public string Url { get; init; } = default!;

    /// <summary>
    /// The raw extracted content from the web page.
    /// </summary>
    public string RawContent { get; init; } = default!;

    /// <summary>
    /// The favicon URL associated with the web page.
    /// </summary>
    public string Favicon { get; init; } = default!;
}

/// <summary>
/// Represents the response from a web crawling operation, including the base URL and a list of results.
/// </summary>
public class Response
{
    /// <summary>
    /// The base URL from which the crawl started.
    /// </summary>
    public string BaseUrl { get; init; } = default!;

    /// <summary>
    /// The list of crawled results.
    /// </summary>
    public List<Result> Results { get; init; } = [];
}
