using MyProfile.Models;

namespace MyProfile.Services.Github;

public interface IGithubHttpClient
{
    public Task<Result<IReadOnlyList<GithubRepo>>> GetReposToBeShown();
    public Task<Result<IReadOnlyList<CommitDisplay>>> GetCommitsForRepoAsync(string repoName);
}