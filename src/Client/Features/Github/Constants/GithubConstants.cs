namespace MyProfile.Features.Github.Constants;
 public static class GithubConstants
    {
        public static class GetRepoInfo
        {
            public const string CacheDataKey = "obaki-site-github-getrepoinfo-cachedata";
            public const string Endpoint = "https://api.github.com/repos/";

        }

        public static class GetLastCommit
        {
            public const string CacheDataKey = "obaki-site-github-getlastcommit-cachedata";
            public const string Endpoint = " https://api.github.com/repos/obaki102/ObakiSite/git/refs/heads/master";

        }

          public const string HttpNameClient = "GithubHttpClient";

    }