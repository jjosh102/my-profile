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
    public async Task<Result<IReadOnlyList<CommitDisplay>>> GetCommitsForRepoAsync(string repoName)
    {
        //todo: simplify constant usage
        var cacheKey = $"{GithubConstants.Commits}-{repoName}";
        var cache = await _localStorageCache.GetOrCreateCacheAsync(
            cacheKey,
            TimeSpan.FromHours(1),
            async () =>
            {
                var response = await _httpClient.GetAsync($"{GithubConstants.GetCommits.Endpoint}/{repoName}/{GithubConstants.Commits}").ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    return Result.Fail<IReadOnlyList<CommitDisplay>>(Error.HttpError(response.StatusCode.ToString()));
                }

                var commits = await response.Content.ReadFromJsonAsync<IReadOnlyList<GithubCommit>>().ConfigureAwait(false);

                if (commits is null)
                {
                    return Result.Fail<IReadOnlyList<CommitDisplay>>(Error.EmptyValue);
                }

                var commitDisplays = commits.Select(c => new CommitDisplay
                {
                    AuthorName = c.Commit.Committer.Name,
                    AuthorAvatarUrl = c.Author?.AvatarUrl ?? string.Empty,
                    CommitDate = c.Commit.Committer.Date,
                    Message = c.Commit.Message,
                    CommitUrl = c.HtmlUrl
                }).ToList();

                return Result.Success<IReadOnlyList<CommitDisplay>>(commitDisplays);
            });

        return cache ?? Result.Fail<IReadOnlyList<CommitDisplay>>(Error.EmptyValue);
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