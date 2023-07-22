namespace MyProfile.Features.ChatGpt;

public interface IChatGptHttpClient
{
     public Task<string> AskChatGpt();
}