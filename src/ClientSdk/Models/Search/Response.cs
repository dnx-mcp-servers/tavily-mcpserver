using System.Text.Json.Serialization;
using Tavily.McpServer.ClientSdk.JsonConverter;

namespace Tavily.McpServer.ClientSdk.Models.Search;

/// <summary>
/// Represents an image result with a URL and optional description.
/// </summary>
public class Image
{
    /// <summary>
    /// The URL of the image.
    /// </summary>
    public string Url { get; init; } = default!;

    /// <summary>
    /// The description of the image, if available.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Implicitly converts a string URL to an <see cref="Image"/> instance.
    /// </summary>
    /// <param name="url">The image URL.</param>
    public static implicit operator Image(string url)
    {
        return new Image { Url = url.Trim() };
    }
}

/// <summary>
/// Represents a single search result item.
/// </summary>
public class Result
{
    /// <summary>
    /// The title of the search result.
    /// </summary>
    public string Title { get; init; } = default!;

    /// <summary>
    /// The URL of the search result.
    /// </summary>
    public string Url { get; init; } = default!;

    /// <summary>
    /// The content snippet of the search result.
    /// </summary>
    public string Content { get; init; } = default!;

    /// <summary>
    /// The raw content extracted from the search result.
    /// </summary>
    public string RawContent { get; init; } = default!;

    /// <summary>
    /// The favicon URL associated with the search result.
    /// </summary>
    public string Favicon { get; init; } = default!;
}

/// <summary>
/// Represents the response for a search query, including results and images.
/// </summary>
public class Response
{
    /// <summary>
    /// The original search query.
    /// </summary>
    public string Query { get; init; } = default!;

    /// <summary>
    /// The answer generated for the search query.
    /// </summary>
    public string Answer { get; init; } = default!;

    /// <summary>
    /// The list of images related to the search query.
    /// </summary>
    [JsonConverter(typeof(ImageJsonConverter))]
    public List<Image> Images { get; init; } = [];

    /// <summary>
    /// The list of search results.
    /// </summary>
    public List<Result> Results { get; init; } = [];
}
