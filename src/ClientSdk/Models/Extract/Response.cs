namespace Tavily.McpServer.ClientSdk.Models.Extract;

/// <summary>
/// Represents a single crawled web page result, including its URL, raw content, images, and favicon.
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
    /// The list of image URLs found on the web page.
    /// </summary>
    public List<string> Images { get; init; } = [];

    /// <summary>
    /// The favicon URL associated with the web page.
    /// </summary>
    public string Favicon { get; init; } = default!;
}

/// <summary>
/// Represents the response containing a list of crawled web page results.
/// </summary>
public class Response
{
    /// <summary>
    /// The list of results from the extraction process.
    /// </summary>
    public List<Result> Results { get; init; } = [];
}
