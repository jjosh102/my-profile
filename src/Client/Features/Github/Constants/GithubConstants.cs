namespace MyProfile.Features.Github.Constants;
 public static class GithubConstants
    {
        public static class GetRepos
        {
            public const string CacheDataKey = "obaki-site-github-getrepos-cachedata";
            public const string Endpoint = "https://api.github.com/users/obaki102/repos";

        }

        public static class GetLastCommit
        {
            public const string CacheDataKey = "obaki-site-github-getlastcommit-cachedata";
            public const string Endpoint = "https://api.github.com/repos/obaki102/MyProfile/git/refs/heads/master";

        }

          public const string HttpNameClient = "GithubHttpClient";

    }