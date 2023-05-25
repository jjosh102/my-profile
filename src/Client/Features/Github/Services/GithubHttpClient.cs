using System.Net.Http.Json;
using MyProfile.Features.Github.Constants;
using MyProfile.Shared.DTO;

namespace MyProfile.Features.Github.Services;

public class GithubHttpClient : IGithubHttpClient
{
    private readonly HttpClient _httpClient;
    public GithubHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(GithubConstants.BaseAddress);
    }
    public async Task<GithubLastCommit> GetRepoLastCommit(string repoName)
    {
        if (string.IsNullOrWhiteSpace(repoName))
        {
            throw new ArgumentNullException(nameof(repoName));
        }

        var uriRequest = $"repos/obaki102/{repoName}/git/refs/heads/master";
        var response = await _httpClient.GetAsync(uriRequest).ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Status code error: {response.StatusCode}");
        }

        var result = await response.Content.ReadFromJsonAsync<GithubLastCommit>().ConfigureAwait(false);
        if (result is null)
        {
            throw new NullReferenceException("No value");
        }

        return result;

    }

    public async Task<IReadOnlyList<GithubRepo>> GetRepos()
    {
        var response = await _httpClient.GetAsync(GithubConstants.GetRepos.Endpoint).ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Status code error: {response.StatusCode}");
        }

        var result = await response.Content.ReadFromJsonAsync<IReadOnlyList<GithubRepo>>().ConfigureAwait(false);
        if (result is null)
        {
            throw new NullReferenceException("No value");
        }

        return result;
    }
}