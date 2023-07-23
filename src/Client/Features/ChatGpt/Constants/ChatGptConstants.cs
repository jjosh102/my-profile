namespace MyProfile.Features.ChatGpt;

public static class ChatGptConstants
{
    public const string BaseAddress = "https://obaki-webapi.onrender.com";
    public const string CacheDataKey = "chat-gpt-response-cachedata";
    public const string Endpoint = "/ask-chatgpt";
    public const string Question = """
                                    create a lively introduction with this information `Josh
                                    .NET Developer Hey there, I'm a developer who has a passion for acquiring fresh knowledge and crafting innovative tools and applications. Having worked for 11 years, I possess expertise in various technologies, but I'm constantly seeking challenging opportunities that enable me to step beyond my comfort zone.
                                    ` limit it to 3 paragraphs.  Be funny and make some jokes.
                                    """;
}