using MyProfile.Shared;
namespace MyProfile.Features.Github;

public interface IGithubHttpClient
{
    public Task<Result<List<GithubRepo>>> GetReposToBeShown();
    public Task<Result<GithubLastCommit>> GetRepoLastCommit(string repoName);
}