using System.Net.Http.Json;
using MyProfile.Features.Github.Constants;
using MyProfile.Shared;
namespace MyProfile.Features.Github;

public class GithubHttpClient : IGithubHttpClient
{
    private readonly HttpClient _httpClient;
    public GithubHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(GithubConstants.BaseAddress);
    }
    public async Task<Result<GithubLastCommit>> GetRepoLastCommit(string repoName)
    {
        if (string.IsNullOrWhiteSpace(repoName))
        {
            throw new ArgumentNullException(nameof(repoName));
        }

        var uriRequest = $"repos/obaki102/{repoName}/git/refs/heads/master";
        var response = await _httpClient.GetAsync(uriRequest).ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
        {
            return Result.Fail<GithubLastCommit>(Error.HttpError(response.StatusCode.ToString()));
        }

        var result = await response.Content.ReadFromJsonAsync<GithubLastCommit>().ConfigureAwait(false);

        if(result is null)
        {
            Result.Fail(Error.EmptyValue);
        }

        return Result.Success(result!);

    }

    public async Task<Result<IReadOnlyList<GithubRepo>>> GetReposToBeShown()
    {
        var response = await _httpClient.GetAsync(GithubConstants.GetRepos.Endpoint).ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
        {
            return Result.Fail<IReadOnlyList<GithubRepo>>(Error.HttpError(response.StatusCode.ToString()));
        }

        var result = await response.Content.ReadFromJsonAsync<IReadOnlyList<GithubRepo>>().ConfigureAwait(false);

        if(result is null)
        {
            Result.Fail(Error.EmptyValue);
        }

        var reposTobeShown = result!.Where(t => t.Topics.Contains("show")).ToList();

        return Result.Success<IReadOnlyList<GithubRepo>>(reposTobeShown);
    }
}