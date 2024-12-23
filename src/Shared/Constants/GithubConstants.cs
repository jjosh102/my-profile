namespace MyProfile.Shared.Constants;
public static class GithubConstants
{

    public const string BaseAddress = "https://api.github.com";
    public const string HttpNameClient = "GithubHttpClient";

    public static class GetRepos
    {
        public const string CacheDataKey = "obaki-site-github-getrepos-cachedata";
        public const string Endpoint = "users/jjosh102/repos";
        public const string Name = "MyProfile";

    }

    public static class GetLastCommit
    {
        public const string CacheDataKey = "obaki-site-github-getlastcommit-cachedata";

        public const string Url = "https://github.com/jjosh102/MyProfile/commit/";
    }

}