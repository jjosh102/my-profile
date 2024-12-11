using MyProfile.Models;

namespace MyProfile.Services.Github;

public interface IGithubHttpClient
{
    public Task<Result<List<GithubRepo>>> GetReposToBeShown();
    public Task<Result<GithubLastCommit>> GetRepoLastCommit(string repoName);
}