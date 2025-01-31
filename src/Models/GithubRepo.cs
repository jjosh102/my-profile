using System.Text.Json.Serialization;

namespace  MyProfile.Models;

    public record Owner
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

    public record License
    {
        [JsonPropertyName("key")]
        public string Key { get; init; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; init; } = string.Empty;

        [JsonPropertyName("spdx_id")]
        public string SpdxId { get; init; } = string.Empty;

        [JsonPropertyName("url")]
        public string Url { get; init; } = string.Empty;

        [JsonPropertyName("node_id")]
        public string NodeId { get; init; } = string.Empty;
    }

    public record GithubRepo
    {
        [JsonPropertyName("id")]
        public int Id { get; init; }

        [JsonPropertyName("node_id")]
        public string NodeId { get; init; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; init; } = string.Empty;

        [JsonPropertyName("full_name")]
        public string FullName { get; init; } = string.Empty;

        [JsonPropertyName("private")]
        public bool Private { get; init; }

        [JsonPropertyName("owner")]
        public Owner Owner { get; init; } = new();

        [JsonPropertyName("html_url")]
        public string HtmlUrl { get; init; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; init; } = string.Empty;

        [JsonPropertyName("fork")]
        public bool Fork { get; init; }

        [JsonPropertyName("url")]
        public string Url { get; init; } = string.Empty;

        [JsonPropertyName("forks_url")]
        public string ForksUrl { get; init; } = string.Empty;

        [JsonPropertyName("keys_url")]
        public string KeysUrl { get; init; } = string.Empty;

        [JsonPropertyName("collaborators_url")]
        public string CollaboratorsUrl { get; init; } = string.Empty;

        [JsonPropertyName("teams_url")]
        public string TeamsUrl { get; init; } = string.Empty;

        [JsonPropertyName("hooks_url")]
        public string HooksUrl { get; init; } = string.Empty;

        [JsonPropertyName("issue_events_url")]
        public string IssueEventsUrl { get; init; } = string.Empty;

        [JsonPropertyName("events_url")]
        public string EventsUrl { get; init; } = string.Empty;

        [JsonPropertyName("assignees_url")]
        public string AssigneesUrl { get; init; } = string.Empty;

        [JsonPropertyName("branches_url")]
        public string BranchesUrl { get; init; } = string.Empty;

        [JsonPropertyName("tags_url")]
        public string TagsUrl { get; init; } = string.Empty;

        [JsonPropertyName("blobs_url")]
        public string BlobsUrl { get; init; } = string.Empty;

        [JsonPropertyName("git_tags_url")]
        public string GitTagsUrl { get; init; } = string.Empty;

        [JsonPropertyName("git_refs_url")]
        public string GitRefsUrl { get; init; } = string.Empty;

        [JsonPropertyName("trees_url")]
        public string TreesUrl { get; init; } = string.Empty;

        [JsonPropertyName("statuses_url")]
        public string StatusesUrl { get; init; } = string.Empty;

        [JsonPropertyName("languages_url")]
        public string LanguagesUrl { get; init; } = string.Empty;

        [JsonPropertyName("stargazers_url")]
        public string StargazersUrl { get; init; } = string.Empty;

        [JsonPropertyName("contributors_url")]
        public string ContributorsUrl { get; init; } = string.Empty;

        [JsonPropertyName("subscribers_url")]
        public string SubscribersUrl { get; init; } = string.Empty;

        [JsonPropertyName("subscription_url")]
        public string SubscriptionUrl { get; init; } = string.Empty;

        [JsonPropertyName("commits_url")]
        public string CommitsUrl { get; init; } = string.Empty;

        [JsonPropertyName("git_commits_url")]
        public string GitCommitsUrl { get; init; } = string.Empty;

        [JsonPropertyName("comments_url")]
        public string CommentsUrl { get; init; } = string.Empty;

        [JsonPropertyName("issue_comment_url")]
        public string IssueCommentUrl { get; init; } = string.Empty;

        [JsonPropertyName("contents_url")]
        public string ContentsUrl { get; init; } = string.Empty;

        [JsonPropertyName("compare_url")]
        public string CompareUrl { get; init; } = string.Empty;

        [JsonPropertyName("merges_url")]
        public string MergesUrl { get; init; } = string.Empty;

        [JsonPropertyName("archive_url")]
        public string ArchiveUrl { get; init; } = string.Empty;

        [JsonPropertyName("downloads_url")]
        public string DownloadsUrl { get; init; } = string.Empty;

        [JsonPropertyName("issues_url")]
        public string IssuesUrl { get; init; } = string.Empty;

        [JsonPropertyName("pulls_url")]
        public string PullsUrl { get; init; } = string.Empty;

        [JsonPropertyName("milestones_url")]
        public string MilestonesUrl { get; init; } = string.Empty;

        [JsonPropertyName("notifications_url")]
        public string NotificationsUrl { get; init; } = string.Empty;

        [JsonPropertyName("labels_url")]
        public string LabelsUrl { get; init; } = string.Empty;

        [JsonPropertyName("releases_url")]
        public string ReleasesUrl { get; init; } = string.Empty;

        [JsonPropertyName("deployments_url")]
        public string DeploymentsUrl { get; init; } = string.Empty;

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; init; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; init; }

        [JsonPropertyName("pushed_at")]
        public DateTime PushedAt { get; init; }

        [JsonPropertyName("git_url")]
        public string GitUrl { get; init; } = string.Empty;

        [JsonPropertyName("ssh_url")]
        public string SshUrl { get; init; } = string.Empty;

        [JsonPropertyName("clone_url")]
        public string CloneUrl { get; init; } = string.Empty;

        [JsonPropertyName("svn_url")]
        public string SvnUrl { get; init; } = string.Empty;

        [JsonPropertyName("homepage")]
        public string Homepage { get; init; } = string.Empty;

        [JsonPropertyName("size")]
        public int Size { get; init; }

        [JsonPropertyName("stargazers_count")]
        public int StargazersCount { get; init; }

        [JsonPropertyName("watchers_count")]
        public int WatchersCount { get; init; }

        [JsonPropertyName("language")]
        public string Language { get; init; } = string.Empty;

        [JsonPropertyName("has_issues")]
        public bool HasIssues { get; init; }

        [JsonPropertyName("has_projects")]
        public bool HasProjects { get; init; }

        [JsonPropertyName("has_downloads")]
        public bool HasDownloads { get; init; }

        [JsonPropertyName("has_wiki")]
        public bool HasWiki { get; init; }

        [JsonPropertyName("has_pages")]
        public bool HasPages { get; init; }

        [JsonPropertyName("has_discussions")]
        public bool HasDiscussions { get; init; }

        [JsonPropertyName("forks_count")]
        public int ForksCount { get; init; }

        [JsonPropertyName("mirror_url")]
        public object MirrorUrl { get; init; } = new();

        [JsonPropertyName("archived")]
        public bool Archived { get; init; }

        [JsonPropertyName("disabled")]
        public bool Disabled { get; init; }

        [JsonPropertyName("open_issues_count")]
        public int OpenIssuesCount { get; init; }

        [JsonPropertyName("license")]
        public License License { get; init; } = new();

        [JsonPropertyName("allow_forking")]
        public bool AllowForking { get; init; }

        [JsonPropertyName("is_template")]
        public bool IsTemplate { get; init; }

        [JsonPropertyName("web_commit_signoff_required")]
        public bool WebCommitSignoffRequired { get; init; }

        [JsonPropertyName("topics")]
        public IReadOnlyList<string> Topics { get; init; } = [];

        [JsonPropertyName("visibility")]
        public string Visibility { get; init; } = string.Empty;

        [JsonPropertyName("forks")]
        public int Forks { get; init; }

        [JsonPropertyName("open_issues")]
        public int OpenIssues { get; init; }

        [JsonPropertyName("watchers")]
        public int Watchers { get; init; }

        [JsonPropertyName("default_branch")]
        public string DefaultBranch { get; init; } = string.Empty;

        [JsonPropertyName("temp_clone_token")]
        public object TempCloneToken { get; init; } = new();

        [JsonPropertyName("network_count")]
        public int NetworkCount { get; init; }

        [JsonPropertyName("subscribers_count")]
        public int SubscribersCount { get; init; }
    }
