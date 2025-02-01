using System.Text.Json.Serialization;

namespace MyProfile.Models;

public record Committer
{
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; init; } = string.Empty;

    [JsonPropertyName("date")]
    public DateTime Date { get; init; }
}

public record Tree
{
    [JsonPropertyName("sha")]
    public string Sha { get; init; } = string.Empty;

    [JsonPropertyName("url")]
    public string Url { get; init; } = string.Empty;
}

public record Verification
{
    [JsonPropertyName("verified")]
    public bool Verified { get; init; }

    [JsonPropertyName("reason")]
    public string Reason { get; init; } = string.Empty;

    [JsonPropertyName("signature")]
    public object Signature { get; init; } = new();

    [JsonPropertyName("payload")]
    public object Payload { get; init; } = new();

    [JsonPropertyName("verified_at")]
    public object VerifiedAt { get; init; } = new();
}

public record Commit
{
    [JsonPropertyName("author")]
    public Author Author { get; init; } = new();

    [JsonPropertyName("committer")]
    public Committer Committer { get; init; } = new();

    [JsonPropertyName("message")]
    public string Message { get; init; } = string.Empty;

    [JsonPropertyName("tree")]
    public Tree Tree { get; init; } = new();

    [JsonPropertyName("url")]
    public string Url { get; init; } = string.Empty;

    [JsonPropertyName("comment_count")]
    public int CommentCount { get; init; }

    [JsonPropertyName("verification")]
    public Verification Verification { get; init; } = new();
}

public record Author
{
    [JsonPropertyName("login")]
    public string Login { get; init; } = string.Empty;

    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("node_id")]
    public string NodeId { get; init; } = string.Empty;

    [JsonPropertyName("avatar_url")]
    public string AvatarUrl { get; init; } = string.Empty;

    [JsonPropertyName("gravatar_id")]
    public string GravatarId { get; init; } = string.Empty;

    [JsonPropertyName("url")]
    public string Url { get; init; } = string.Empty;

    [JsonPropertyName("html_url")]
    public string HtmlUrl { get; init; } = string.Empty;

    [JsonPropertyName("followers_url")]
    public string FollowersUrl { get; init; } = string.Empty;

    [JsonPropertyName("following_url")]
    public string FollowingUrl { get; init; } = string.Empty;

    [JsonPropertyName("gists_url")]
    public string GistsUrl { get; init; } = string.Empty;

    [JsonPropertyName("starred_url")]
    public string StarredUrl { get; init; } = string.Empty;

    [JsonPropertyName("subscriptions_url")]
    public string SubscriptionsUrl { get; init; } = string.Empty;

    [JsonPropertyName("organizations_url")]
    public string OrganizationsUrl { get; init; } = string.Empty;

    [JsonPropertyName("repos_url")]
    public string ReposUrl { get; init; } = string.Empty;

    [JsonPropertyName("events_url")]
    public string EventsUrl { get; init; } = string.Empty;

    [JsonPropertyName("received_events_url")]
    public string ReceivedEventsUrl { get; init; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    [JsonPropertyName("user_view_type")]
    public string UserViewType { get; init; } = string.Empty;

    [JsonPropertyName("site_admin")]
    public bool SiteAdmin { get; init; }
}



public record GithubCommit
{
    [JsonPropertyName("sha")]
    public string Sha { get; init; } = string.Empty;

    [JsonPropertyName("node_id")]
    public string NodeId { get; init; } = string.Empty;

    [JsonPropertyName("commit")]
    public Commit Commit { get; init; } = new();

    [JsonPropertyName("url")]
    public string Url { get; init; } = string.Empty;

    [JsonPropertyName("html_url")]
    public string HtmlUrl { get; init; } = string.Empty;

    [JsonPropertyName("comments_url")]
    public string CommentsUrl { get; init; } = string.Empty;

    [JsonPropertyName("author")]
    public Author Author { get; init; } = new();

    [JsonPropertyName("committer")]
    public Committer Committer { get; init; } = new();

    [JsonPropertyName("parents")]
    public IReadOnlyList<object> Parents { get; init; } = [];
}
