using System.ComponentModel.DataAnnotations;

namespace Tavily.McpServer.ClientSdk.Configuration;

/// <summary>
/// Represents configuration options for the Tavily client.
/// </summary>
public class TavilyClientOptions
{
    /// <summary>
    /// The configuration section name for Tavily client options.
    /// </summary>
    public const string TavilyClientOptionName = "TavilyClientOption";

    /// <summary>
    /// The base address for the Tavily API. Defaults to the value of the TAVILY_BASE_ADDRESS environment variable, or "https://api.tavily.com" if not set.
    /// </summary>
    public string BaseAddress { get; init; } = Environment.GetEnvironmentVariable("TAVILY_BASE_ADDRESS") ?? "https://api.tavily.com"!;

    /// <summary>
    /// The API key for authenticating with the Tavily API. Must be provided via the TAVILY_API_KEY environment variable or configuration.
    /// </summary>
    [Required]
    public string ApiKey { get; init; } = Environment.GetEnvironmentVariable("TAVILY_API_KEY")  ?? default!;
}
