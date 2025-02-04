using System.Net.Http.Json;
using System.Text.Json;
using MyProfile.Models;
using Obaki.LocalStorageCache;
using Polly;

namespace MyProfile.Services.Github;

internal sealed class GithubHttpClient : IGithubHttpClient
{
    public const string BaseAddress = "https://api.github.com";
    public const string CommitsEndpoint = "commits";
    public const string CodeFrequencyEndpoint = "code-frequency";
    public const string LanguagesEndpoint = "languages";
    public const string ProxyApi = "https://obaki-core.onrender.com/api/v1/github-proxy";
    public const string ReposEndpoint = "repos";
    public const string UserReposEndpoint = "users/jjosh102/repos";
    public const string RepoEndpoint = "repos/jjosh102";
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageCache _localStorageCache;

    public GithubHttpClient(HttpClient httpClient, ILocalStorageCache localStorageCache)
    {
        _httpClient = httpClient;
        _localStorageCache = localStorageCache;
    }

    private async Task<Result<T>> FetchAndCacheAsync<T>(string cacheKey, string endpoint, TimeSpan cacheDuration)
    {
        try
        {
            return await _localStorageCache.GetOrCreateCacheAsync(
                cacheKey,
                cacheDuration,
                async () =>
                {
                    var url = $"{BaseAddress}/{endpoint}";
                    var response = await _httpClient.GetAsync(url).ConfigureAwait(false);

                    // If the rate limit is exceeded, use the proxy API to fetch the data
                    // This will take a while since the proxy API has a chance to wait for a cold start
                    if (response.StatusCode == System.Net.HttpStatusCode.Forbidden &&
                         response.Headers.Contains("X-RateLimit-Remaining") &&
                         response.Headers.GetValues("X-RateLimit-Remaining").FirstOrDefault() == "0")
                    {
                        var proxyUrl = $"{ProxyApi}?url={url}";
                        response = await _httpClient.GetAsync(proxyUrl).ConfigureAwait(false);
                    }

                    if (!response.IsSuccessStatusCode)
                    {
                        return Result.Fail<T>(Error.HttpError(response.StatusCode.ToString()));
                    }

                    var data = await response.Content.ReadFromJsonAsync<T>().ConfigureAwait(false);
                    return data is null ? Result.Fail<T>(Error.EmptyValue) : Result.Success(data);
                }) ?? Result.Fail<T>(Error.EmptyValue);
        }
        catch (JsonException)
        {
            return Result.Fail<T>(Error.EmptyValue);
        }
        catch (Exception ex)
        {
            return Result.Fail<T>(Error.HttpError(ex.Message));
        }
    }

    public async Task<Result<IReadOnlyList<CommitDisplay>>> GetCommitsForRepoAsync(string repoName)
    {
        var cacheKey = $"{CommitsEndpoint}-{repoName}";
        var endpoint = $"{RepoEndpoint}/{repoName}/{CommitsEndpoint}";

        var result = await FetchAndCacheAsync<IReadOnlyList<GithubCommit>>(cacheKey, endpoint, TimeSpan.FromHours(1));
        if (result.IsFailure || result.Value is null)
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

    public async Task<Result<IReadOnlyList<GithubRepo>>> GetReposToBeShown()
    {
        var result = await FetchAndCacheAsync<IReadOnlyList<GithubRepo>>(ReposEndpoint,
            UserReposEndpoint, TimeSpan.FromHours(1));

        if (result.IsFailure || result.Value is null)
        {
            return Result.Fail<IReadOnlyList<GithubRepo>>(Error.EmptyValue);
        }

        return Result.Success<IReadOnlyList<GithubRepo>>(result.Value.Where(t => t.Topics.Contains("show"))
            .OrderByDescending(t => t.UpdatedAt).ToList());
    }

    public async Task<Result<IReadOnlyList<int[]>>> GetCodeFrequencyStatsAsync(string repoName)
    {
        var cacheKey = $"{CodeFrequencyEndpoint}-{repoName}";
        var endpoint = $"{RepoEndpoint}/{repoName}/stats/code_frequency";

        // We will try to fetch the data 3 times, allowing GitHub to process the stats data for the first time
        var policy = Policy
            .HandleResult<Result<IReadOnlyList<int[]>>>(result => result is null || result.IsFailure || result.Value is null)
            .WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(2));

        var result = await policy.ExecuteAsync(() =>
            FetchAndCacheAsync<IReadOnlyList<int[]>>(cacheKey, endpoint, TimeSpan.FromHours(1)));

        if (result is null || result.IsFailure || result.Value is null)
        {
            return Result.Fail<IReadOnlyList<int[]>>(Error.EmptyValue);
        }

        return result;
    }


    public async Task<Result<Dictionary<string, int>>> GetLanguagesUsedAsync(string repoName)
    {
        var cacheKey = $"{LanguagesEndpoint}-{repoName}";
        var endpoint = $"{RepoEndpoint}/{repoName}/{LanguagesEndpoint}";
        return await FetchAndCacheAsync<Dictionary<string, int>>(cacheKey, endpoint, TimeSpan.FromHours(1));
    }
}