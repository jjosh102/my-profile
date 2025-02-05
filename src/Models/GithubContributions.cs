using System.Text.Json.Serialization;

namespace MyProfile.Models;
public record Contribution
{
    [JsonPropertyName("date")]
    public DateTime Date { get; init; }

    [JsonPropertyName("contributionCount")]
    public int ContributionCount { get; init; }
}

public record GithubContributions
{
    [JsonPropertyName("contributions")]
    public IReadOnlyList<Contribution> Contributions { get; init; } = [];
}