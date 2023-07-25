using MyProfile.Shared;
namespace MyProfile.Features.ChatGpt;

public interface IChatGptHttpClient
{
     public Task<Result<string>> AskChatGpt();
}