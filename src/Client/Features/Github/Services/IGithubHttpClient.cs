using MyProfile.Shared;
namespace MyProfile.Features.Github;

public interface IGithubHttpClient
{
    public Task<Result<IReadOnlyList<GithubRepo>>> GetReposToBeShown();
    public Task<Result<GithubLastCommit>> GetRepoLastCommit(string repoName);
}