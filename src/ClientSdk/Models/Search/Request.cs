using System.ComponentModel;

namespace Tavily.McpServer.ClientSdk.Models.Search;

/// <summary>
/// Specifies the depth of the search operation.
/// </summary>
public enum SearchDepth
{
    /// <summary>
    /// Basic search depth.
    /// </summary>
    Basic,
    /// <summary>
    /// Advanced search depth.
    /// </summary>
    Advanced
}

/// <summary>
/// Specifies the topic or category of the search.
/// </summary>
public enum SearchTopic
{
    /// <summary>
    /// General search topic.
    /// </summary>
    General,
    /// <summary>
    /// News search topic.
    /// </summary>
    News
}

/// <summary>
/// Specifies the time range for the search results.
/// </summary>
public enum TimeRange
{
    /// <summary>
    /// Search within the last day.
    /// </summary>
    Day,
    /// <summary>
    /// Search within the last week.
    /// </summary>
    Week,
    /// <summary>
    /// Search within the last month.
    /// </summary>
    Month,
    /// <summary>
    /// Search within the last year.
    /// </summary>
    Year,
    /// <summary>
    /// Search within the last day (short form).
    /// </summary>
    D,
    /// <summary>
    /// Search within the last week (short form).
    /// </summary>
    W,
    /// <summary>
    /// Search within the last month (short form).
    /// </summary>
    M,
    /// <summary>
    /// Search within the last year (short form).
    /// </summary>
    Y
}

/// <summary>
/// Provides string constants for time range options.
/// </summary>
public static class TimeRangeOptions
{
    /// <summary>
    /// Represents the 'day' time range.
    /// </summary>
    public const string Day = "day";
    /// <summary>
    /// Represents the 'week' time range.
    /// </summary>
    public const string Week = "week";
    /// <summary>
    /// Represents the 'month' time range.
    /// </summary>
    public const string Month = "month";
    /// <summary>
    /// Represents the 'year' time range.
    /// </summary>
    public const string Year = "year";
    /// <summary>
    /// Represents the 'd' (day) time range.
    /// </summary>
    public const string D = "d";
    /// <summary>
    /// Represents the 'w' (week) time range.
    /// </summary>
    public const string W = "w";
    /// <summary>
    /// Represents the 'm' (month) time range.
    /// </summary>
    public const string M = "m";
    /// <summary>
    /// Represents the 'y' (year) time range.
    /// </summary>
    public const string Y = "y";
}

/// <summary>
/// Request model for web search operation.
/// </summary>
public record Request
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Request"/> record.
    /// </summary>
    public Request()
    {
        // If Country is set, default Topic to General
        if (!string.IsNullOrEmpty(Country))
        {
            Topic = SearchTopic.General;
        }
    }

    /// <summary>
    /// Search query.
    /// </summary>
    [Description("Search query")]
    public required string Query { get; init; }

    /// <summary>
    /// The depth of the search. It can be 'Basic' or 'Advanced'.
    /// </summary>
    [Description("The depth of the search. It can be 'Basic' or 'Advanced'")]
    public SearchDepth  SearchDepth { get; init; } = SearchDepth.Basic;

    /// <summary>
    /// Whether to include an LLM-generated answer to the provided query.
    /// </summary>
    [Description("Whether to include an LLM-generated answer to the provided query. ")]
    public bool IncludeAnswer { get; init; } = false;

    /// <summary>
    /// The category of the search. Determines which agent will be used for the search. It can be 'General' or 'News'.
    /// </summary>
    [Description("The category of the search. This will determine which of our agents will be used for the search.It can be 'General' or 'News'")]
    public SearchTopic Topic { get; init; } = SearchTopic.General;

    /// <summary>
    /// The number of days back from the current date to include in the search results. Only available for 'News' topic.
    /// </summary>
    [Description("The number of days back from the current date to include in the search results. This specifies the time frame of data to be retrieved. Please note that this feature is only available when using the 'News' search topic")]
    public int Days { get; init; } = 3;

    /// <summary>
    /// The time range back from the current date to include in the search results. Available for both 'General' and 'News' topics.
    /// </summary>
    [Description("The time range back from the current date to include in the search results. This feature is available for both 'General' and 'News' search topics")]
    public TimeRange? TimeRange { get; init; }

    /// <summary>
    /// Will return all results after the specified start date. Format: YYYY-MM-DD.
    /// </summary>
    [Description("Will return all results after the specified start date. Required to be written in the format YYYY-MM-DD.")]
    public string? StartDate { get; init; } = string.Empty;

    /// <summary>
    /// Will return all results before the specified end date. Format: YYYY-MM-DD.
    /// </summary>
    [Description("Will return all results before the specified end date. Required to be written in the format YYYY-MM-DD")]
    public string? EndDate { get; init; } = string.Empty;

    /// <summary>
    /// The maximum number of search results to return. Maximum 20, minimum 5.
    /// </summary>
    [Description("The maximum number of search results to return, It can be maximum 20 and minimum 5")]
    public int MaxResults { get; init; } = 10;

    /// <summary>
    /// Include a list of query-related images in the response.
    /// </summary>
    [Description("Include a list of query-related images in the response")]
    public bool IncludeImages { get; init; } = false;

    /// <summary>
    /// Include a list of query-related images and their descriptions in the response.
    /// </summary>
    [Description("Include a list of query-related images and their descriptions in the response")]
    public bool IncludeImageDescriptions { get; init; } = false;

    /// <summary>
    /// Include the cleaned and parsed HTML content of each search result.
    /// </summary>
    [Description("Include the cleaned and parsed HTML content of each search result")]
    public bool IncludeRawContent { get; init; } = false;

    /// <summary>
    /// A list of domains to specifically include in the search results.
    /// </summary>
    [Description("A list of domains to specifically include in the search results, if the user asks to search on specific sites set this to the domain of the site")]
    public List<string> IncludeDomains { get; init; } = [];

    /// <summary>
    /// List of domains to specifically exclude from the search results.
    /// </summary>
    [Description("List of domains to specifically exclude, if the user asks to exclude a domain set this to the domain of the site")]
    public List<string> ExcludeDomains { get; init; } = [];

    /// <summary>
    /// Boost search results from a specific country. Only available if topic is general. Country names must be lowercase, plain English, with spaces and no underscores.
    /// </summary>
    [Description("Boost search results from a specific country.This will prioritize content from the selected country in the search results.Available only if topic is general.Country names MUST be written in lowercase, plain English, with spaces and no underscores.")]
    public string? Country { get; init; }

    /// <summary>
    /// Whether to include the favicon URL for each result.
    /// </summary>
    [Description("Whether to include the favicon URL for each result")]
    public bool IncludeFavicon { get; init; } = false;
}
