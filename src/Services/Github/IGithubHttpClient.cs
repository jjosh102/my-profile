using MyProfile.Models;

namespace MyProfile.Services.Github;

public interface IGithubHttpClient
{
    Task<Result<IReadOnlyList<GithubRepo>>> GetReposToBeShownAsync();
    Task<Result<IReadOnlyList<CommitDisplay>>> GetCommitsForRepoAsync(string repoName);
    Task<Result<IReadOnlyList<int[]>>> GetCodeFrequencyStatsAsync(string repoName);
    Task<Result<Dictionary<string, int>>> GetLanguagesUsedAsync(string repoName);
    Task<Result<GithubContributions>> GetContributionsAsync();
}