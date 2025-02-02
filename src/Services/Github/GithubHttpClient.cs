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

    private async Task<Result<T>> FetchAndCacheAsync<T>(string cacheKey, string endpoint, TimeSpan cacheDuration, Func<T, T>? transform = null)
    {
        try
        {
            return await _localStorageCache.GetOrCreateCacheAsync(
                cacheKey,
                cacheDuration,
                async () =>
                {
                    var response = await _httpClient.GetAsync(endpoint).ConfigureAwait(false);
                    if (!response.IsSuccessStatusCode)
                    {
                        return Result.Fail<T>(Error.HttpError(response.StatusCode.ToString()));
                    }

                    var data = await response.Content.ReadFromJsonAsync<T>().ConfigureAwait(false);
                    return data is null ? Result.Fail<T>(Error.EmptyValue) : Result.Success(transform != null ? transform(data) : data);
                }) ?? Result.Fail<T>(Error.EmptyValue);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Result.Fail<T>(Error.HttpError(ex.Message));
        }
    }

    public async Task<Result<IReadOnlyList<CommitDisplay>>> GetCommitsForRepoAsync(string repoName)
    {
        var cacheKey = $"{GithubConstants.Commits}-{repoName}";
        var endpoint = $"{GithubConstants.GetCommits.Endpoint}/{repoName}/{GithubConstants.Commits}";

        var result = await FetchAndCacheAsync<IReadOnlyList<GithubCommit>>(cacheKey, endpoint, TimeSpan.FromHours(1));
        if (!result.IsSuccess || result.Value is null)
        {
            return Result.Fail<IReadOnlyList<CommitDisplay>>(result.Error);
        }

        var commits = result.Value.Select(c => new CommitDisplay
        {
            AuthorName = c.Commit.Committer.Name,
            AuthorAvatarUrl = c.Author?.AvatarUrl ?? string.Empty,
            CommitDate = c.Commit.Committer.Date,
            Message = c.Commit.Message,
            CommitUrl = c.HtmlUrl
        }).ToList();

        return Result.Success<IReadOnlyList<CommitDisplay>>(commits);
    }

    public Task<Result<IReadOnlyList<GithubRepo>>> GetReposToBeShown()
    {
        return FetchAndCacheAsync<IReadOnlyList<GithubRepo>>(GithubConstants.GetRepos.CacheDataKey, GithubConstants.GetRepos.Endpoint, TimeSpan.FromHours(1),
            repos => repos.Where(t => t.Topics.Contains("show")).OrderByDescending(t => t.UpdatedAt).ToList());
    }

    public Task<Result<IReadOnlyList<int[]>>> GetCodeFrequencyStatsAsync(string repoName)
    {
        var cacheKey = $"{GithubConstants.CodeFrequency}-{repoName}";
        var endpoint = $"{GithubConstants.BaseAddress}/repos/jjosh102/{repoName}/stats/code_frequency";
        return FetchAndCacheAsync<IReadOnlyList<int[]>>(cacheKey, endpoint, TimeSpan.FromHours(1));
    }

    public Task<Result<Dictionary<string, int>>> GetLanguagesUsedAsync(string repoName)
    {
        var cacheKey = $"{GithubConstants.Languages}-{repoName}";
        var endpoint = $"{GithubConstants.GetCommits.Endpoint}/{repoName}/{GithubConstants.Languages}";
        return FetchAndCacheAsync<Dictionary<string, int>>(cacheKey, endpoint, TimeSpan.FromHours(1));
    }
}
