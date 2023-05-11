using System.Net.Http.Json;
using MediatR;
using MyProfile.Features.Github.Constants;
using MyProfile.Shared;
using MyProfile.Shared.DTO;

namespace MyProfile.Features.Github.Queries;
 public record GetRepoInfo(string user, string repository) : IRequest<Result<GithubRepoInfo>>;

    public class GetRepoInfoHandler : IRequestHandler<GetRepoInfo, Result<GithubRepoInfo>>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public GetRepoInfoHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Result<GithubRepoInfo>> Handle(GetRepoInfo request, CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient(GithubConstants.HttpNameClient);
            var uriRequest = $"{GithubConstants.GetRepoInfo.Endpoint}{request.user}/{request.repository}";
            var response = await httpClient.GetAsync(uriRequest).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<GithubRepoInfo>().ConfigureAwait(false);

                if (result is null)
                {
                    return Result.Fail<GithubRepoInfo>(Error.EmptyValue);
                }

                return result;
            }
            return Result.Fail<GithubRepoInfo>(Error.EmptyValue);
        }

    }