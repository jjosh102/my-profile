namespace MyProfile.Features.Github.Constants;
 public static class GithubConstants
    {

        public const string BaseAddress = "https://api.github.com";
        public static class GetRepos
        {
            public const string CacheDataKey = "obaki-site-github-getrepos-cachedata";
            public const string Endpoint = "users/obaki102/repos";
        }

        public static class GetLastCommit
        {
            public const string CacheDataKey = "obaki-site-github-getlastcommit-cachedata";
        }
          public const string HttpNameClient = "GithubHttpClient";
    }