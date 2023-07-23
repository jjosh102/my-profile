using System.Net.Http.Json;
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
    public async Task<string> AskChatGpt()
    {
        var cache = await _localStorageCache.GetOrCreateCacheAsync(
                ChatGptConstants.CacheDataKey,
                TimeSpan.FromHours(1),
               async () =>
               {
                   var question = new ChatGptRequest(ChatGptConstants.Question);
                   var response = await _httpClient.PostAsJsonAsync(ChatGptConstants.Endpoint, question).ConfigureAwait(false);

                   if (!response.IsSuccessStatusCode)
                   {
                       throw new Exception($"Status code error: {response.StatusCode}");
                   }
                   return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
               });

        return cache ?? throw new NullReferenceException("No value");
    }
}