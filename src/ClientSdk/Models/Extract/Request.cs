using System.ComponentModel;

namespace Tavily.McpServer.ClientSdk.Models.Extract;

/// <summary>
/// Specifies the depth of extraction for web content.
/// </summary>
public enum ExtractDepth
{
    /// <summary>
    /// Basic extraction mode.
    /// </summary>
    Basic,
    /// <summary>
    /// Advanced extraction mode, recommended for LinkedIn or when explicitly required.
    /// </summary>
    Advanced
}

/// <summary>
/// Specifies the format of the extracted web page content.
/// </summary>
public enum ContentFormat
{
    /// <summary>
    /// Markdown format for extracted content.
    /// </summary>
    Markdown,
    /// <summary>
    /// Plain text format for extracted content.
    /// </summary>
    Text
}

/// <summary>
/// Request model for web content extraction operation
/// </summary>
public record Request
{
    /// <summary>
    /// List of URLs to extract content from
    /// </summary>
    [Description("List of URLs to extract content from")]
    public required string[] Urls { get; init; }

    /// <summary>
    /// Depth of extraction - 'basic' or 'advanced', if urls are linkedin use 'advanced' or if explicitly told to use advanced
    /// </summary>
    [Description("Depth of extraction - 'basic' or 'advanced', if urls are linkedin use 'advanced' or if explicitly told to use advanced")]
    public ExtractDepth ExtractDepth { get; init; } = ExtractDepth.Basic;

    /// <summary>
    /// Include a list of images extracted from the urls in the response
    /// </summary>
    [Description("Include a list of images extracted from the urls in the response")]
    public bool IncludeImages { get; init; } = false;

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
