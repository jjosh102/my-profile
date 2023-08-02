using System.Net.Http.Json;
using MyProfile.Shared;
using Obaki.LocalStorageCache;
namespace MyProfile.Features.ChatGpt;
public class ChatGptHttpClient : IChatGptHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageCache _localStorageCache;

    public ChatGptHttpClient(HttpClient httpClient, ILocalStorageCache localStorageCache)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(ChatGptConstants.BaseAddress);
        _localStorageCache = localStorageCache;
    }
    public async Task<Result<string>> AskChatGpt()
    {
        var cache = await _localStorageCache.GetOrCreateCacheAsync(
                ChatGptConstants.CacheDataKey,
                TimeSpan.FromDays(7),
               async () =>
               {
                   var question = new ChatGptRequest(ChatGptConstants.Question);
                   var response = await _httpClient.PostAsJsonAsync(ChatGptConstants.Endpoint, question).ConfigureAwait(false);

                   if (!response.IsSuccessStatusCode)
                   {
                       return Result.Fail<string>(Error.HttpError(response.StatusCode.ToString()));
                   }
                   return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
               });

        return cache ??  Result.Fail<string>(Error.EmptyValue);
    }
}