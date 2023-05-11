using System.Net.Http.Json;
using MediatR;
using MyProfile.Features.Github.Constants;
using MyProfile.Shared;
using MyProfile.Shared.DTO;

namespace MyProfile.Features.Github.Queries;

 public record GetLastCommit() : IRequest<Result<GithubLastCommit>>;

    public class GetLastCommitHandler : IRequestHandler<GetLastCommit, Result<GithubLastCommit>>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public GetLastCommitHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Result<GithubLastCommit>> Handle(GetLastCommit request, CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient(GithubConstants.HttpNameClient);
            var uriRequest = GithubConstants.GetLastCommit.Endpoint;
            var response = await httpClient.GetAsync(uriRequest).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<GithubLastCommit>().ConfigureAwait(false);

                if (result is null)
                {
                    return Result.Fail<GithubLastCommit>(Error.EmptyValue);
                }

                return result;
            }
            return Result.Fail<GithubLastCommit>(Error.EmptyValue);
        }
    }