using System.Net.Http.Json;

using MyProfile.Models;
using MyProfile.Shared.Constants;
using Obaki.LocalStorageCache;

namespace MyProfile.Services.Github;

internal sealed class GithubHttpClient : IGithubHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageCache _localStorageCache;
    public GithubHttpClient(HttpClient httpClient, ILocalStorageCache localStorageCache)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(GithubConstants.BaseAddress);
        _localStorageCache = localStorageCache;
    }
    public async Task<Result<GithubLastCommit>> GetRepoLastCommit(string repoName)
    {
        if (string.IsNullOrWhiteSpace(repoName))
        {
            throw new ArgumentNullException(nameof(repoName));
        }

        var uriRequest = $"repos/jjosh102/{repoName}/git/refs/heads/master";
        var response = await _httpClient.GetAsync(uriRequest).ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
        {
            return Result.Fail<GithubLastCommit>(Error.HttpError(response.StatusCode.ToString()));
        }

        var result = await response.Content.ReadFromJsonAsync<GithubLastCommit>().ConfigureAwait(false);

        if (result is null)
        {
            Result.Fail(Error.EmptyValue);
        }

        return Result.Success(result!);

    }

    public async Task<Result<IReadOnlyList<GithubRepo>>> GetReposToBeShown()
    {
        var cache = await _localStorageCache.GetOrCreateCacheAsync(
            GithubConstants.GetRepos.CacheDataKey,
            TimeSpan.FromHours(1),
              async () =>
              {
                  var response = await _httpClient.GetAsync(GithubConstants.GetRepos.Endpoint).ConfigureAwait(false);

                  if (!response.IsSuccessStatusCode)
                  {
                      return Result.Fail<IReadOnlyList<GithubRepo>>(Error.HttpError(response.StatusCode.ToString()));
                  }

                  var result = await response.Content.ReadFromJsonAsync<IReadOnlyList<GithubRepo>>().ConfigureAwait(false);

                  if (result is null)
                  {
                      Result.Fail(Error.EmptyValue);
                  }

                  var reposTobeShown = result!
                      .Where(t => t.Topics.Contains("show"))
                      .OrderByDescending(t => t.UpdatedAt)
                      .ToList();

                  return Result.Success<IReadOnlyList<GithubRepo>>(reposTobeShown);
              });

        return cache ?? Result.Fail<IReadOnlyList<GithubRepo>>(Error.EmptyValue);
    }
}