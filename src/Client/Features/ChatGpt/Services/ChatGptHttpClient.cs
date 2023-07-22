using System.Net.Http.Json;
namespace MyProfile.Features.ChatGpt;
public class ChatGptHttpClient : IChatGptHttpClient
{
    private readonly HttpClient _httpClient;

    public ChatGptHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(ChatGptConstants.BaseAddress);
    }
    public async Task<string> AskChatGpt()
    {
        var question = new ChatGptRequest(ChatGptConstants.Question);
        var response = await _httpClient.PostAsJsonAsync(ChatGptConstants.Endpoint,question).ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Status code error: {response.StatusCode}");
        }

        var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        return result ?? throw new NullReferenceException("No value");
    }
}