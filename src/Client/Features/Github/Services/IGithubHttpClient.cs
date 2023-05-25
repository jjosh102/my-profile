using MyProfile.Shared.DTO;

namespace MyProfile.Features.Github.Services;

public interface IGithubHttpClient
{
    public  Task<IReadOnlyList<GithubRepo>> GetRepos();
     public Task<GithubLastCommit> GetRepoLastCommit(string repoName);
}  