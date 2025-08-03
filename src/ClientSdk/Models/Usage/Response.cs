namespace Tavily.McpServer.ClientSdk.Models.Usage;

/// <summary>
/// Represents usage and limit information for an API key.
/// </summary>
public class KeyInfo
{
    /// <summary>
    /// The current usage for the API key.
    /// </summary>
    public double? Usage { get; init; }

    /// <summary>
    /// The usage limit for the API key.
    /// </summary>
    public double? Limit { get; init; }
}

/// <summary>
/// Represents account plan and usage information.
/// </summary>
public class AccountInfo
{
    /// <summary>
    /// The name of the current plan.
    /// </summary>
    public string CurrentPlan { get; init; } = default!;

    /// <summary>
    /// The usage for the current plan.
    /// </summary>
    public double? PlanUsage { get; init; }

    /// <summary>
    /// The usage limit for the current plan.
    /// </summary>
    public double? PlanLimit { get; init; }

    /// <summary>
    /// The pay-as-you-go usage.
    /// </summary>
    public double? PaygoUsage { get; init; }

    /// <summary>
    /// The pay-as-you-go usage limit.
    /// </summary>
    public double? PaygoLimit { get; init; }
}

/// <summary>
/// Represents the response containing key and account usage information.
/// </summary>
public class Response
{
    /// <summary>
    /// The API key usage and limit information.
    /// </summary>
    public KeyInfo Key { get; init; } = default!;

    /// <summary>
    /// The account plan and usage information.
    /// </summary>
    public AccountInfo Account { get; init; } = default!;
}
