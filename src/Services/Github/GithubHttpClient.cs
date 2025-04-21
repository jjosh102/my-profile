using System.Net.Http.Json;
using System.Text.Json;
using Blazored.LocalStorage;
using BrowserCache.Extensions.LocalStorage;
using MyProfile.Models;
using Polly;
using Polly.Contrib.WaitAndRetry;
namespace MyProfile.Services.Github;

internal sealed class GithubHttpClient : IGithubHttpClient
{
    private const string BaseAddress = "https://api.github.com";
    private const string ProxyBaseAddress = "https://obaki-core.onrender.com";
    private const string CommitsEndpoint = "commits";
    private const string CodeFrequencyEndpoint = "code-frequency";
    private const string LanguagesEndpoint = "languages";
    private const string ProxyApi = "https://obaki-core.onrender.com/api/v1/github-proxy";
    private const string ReposEndpoint = "repos";
    private const string UserReposEndpoint = "users/jjosh102/repos";
    private const string RepoEndpoint = "repos/jjosh102";
    private const string ContributionsEndpoint = "api/v1/github-contributions";
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorageCache;

    public GithubHttpClient(HttpClient httpClient, ILocalStorageService localStorageCache)
    {
        _httpClient = httpClient;
        _localStorageCache = localStorageCache;
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

    public async Task<Result<IReadOnlyList<GithubRepo>>> GetReposToBeShownAsync()
    {
        var result = await FetchAndCacheAsync<IReadOnlyList<GithubRepo>>(
            ReposEndpoint,
            UserReposEndpoint,
            TimeSpan.FromHours(1));

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

        // We will try to fetch the data 5 times, allowing GitHub to process the stats data for the first time
        // The delay will start with 3 seconds and increase linearly with each retry
        var delay = Backoff.LinearBackoff(TimeSpan.FromSeconds(3), 5);

        var policy = Policy
            .HandleResult<Result<IReadOnlyList<int[]>>>(result => result is null || result.IsFailure || result.Value is null)
            .WaitAndRetryAsync(delay);

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
        return await FetchAndCacheAsync<Dictionary<string, int>>(cacheKey, endpoint, TimeSpan.FromDays(30));
    }

    public async Task<Result<GithubContributions>> GetContributionsAsync()
    {
        var cacheKey = $"contributions-data";
        return await FetchAndCacheAsync<GithubContributions>(cacheKey, ContributionsEndpoint, TimeSpan.FromDays(1), true);
    }

    private async Task<Result<T>> FetchAndCacheAsync<T>(string cacheKey, string endpoint, TimeSpan cacheDuration, bool useProxy = false)
    {
        try
        {
            return await _localStorageCache.GetOrCreateCacheAsync(
                cacheKey,
                cacheDuration,
                async () =>
                {
                    var url = $"{(useProxy ? ProxyBaseAddress : BaseAddress)}/{endpoint}";
                    var response = await _httpClient.GetAsync(url).ConfigureAwait(false);

                    // If the rate limit is exceeded, use the proxy API to fetch the data
                    // This will take a while since the proxy API has a chance to wait for a cold start
                    if (response.StatusCode == System.Net.HttpStatusCode.Forbidden &&
                         response.Headers.Contains("X-RateLimit-Remaining") &&
                         response.Headers.GetValues("X-RateLimit-Remaining").FirstOrDefault() == "0" &&
                         !useProxy)
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
}