namespace MyProfile.Shared.Constants;
public static class GithubConstants
{

    public const string BaseAddress = "https://api.github.com";
    public const string HttpNameClient = "GithubHttpClient";
    public const string Commits = "commits";
    public const string CodeFrequency = "code-frequency";
    public const string Languages = "languages";
    public const string ProxyApi = "https://obaki-core.onrender.com/api/v1/github-proxy";

    public static class GetRepos
    {
        public const string CacheDataKey = "obaki-site-github-getrepos-cachedata";
        public const string Endpoint = "users/jjosh102/repos";
        public const string Name = "MyProfile";

    }

    public static class GetCommits
    {
        public const string Endpoint = "repos/jjosh102";
    }

}