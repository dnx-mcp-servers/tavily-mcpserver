namespace Tavily.McpServer.ClientSdk.Models.Map;

/// <summary>
/// Represents a response containing a base URL and a list of result strings.
/// </summary>
public class Response
{
    /// <summary>
    /// Gets the base URL associated with the response.
    /// </summary>
    public string BaseUrl { get; init; } = default!;

    /// <summary>
    /// Gets the list of result strings returned in the response.
    /// </summary>
    public List<string> Results { get; init; } = [];
}
