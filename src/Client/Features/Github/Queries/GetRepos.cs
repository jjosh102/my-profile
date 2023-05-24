using System.Net.Http.Json;
using MediatR;
using MyProfile.Features.Github.Constants;
using MyProfile.Shared;
using MyProfile.Shared.DTO;

namespace MyProfile.Features.Github.Queries;
 public record GetHighlightedRepos : IRequest<Result<IReadOnlyList<GithubRepo>>>;

    public class GetReposHandler : IRequestHandler<GetHighlightedRepos, Result<IReadOnlyList<GithubRepo>>>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public GetReposHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Result<IReadOnlyList<GithubRepo>>> Handle(GetHighlightedRepos request, CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient(GithubConstants.HttpNameClient);
            var response = await httpClient.GetAsync(GithubConstants.GetRepos.Endpoint).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<IReadOnlyList<GithubRepo>>().ConfigureAwait(false);

                if (result is null)
                {
                    return Result.Fail<IReadOnlyList<GithubRepo>>("No result.");
                }
                var highlightedProjects =  result.Where(t => t.Topics.Contains("show")).ToList();
                return Result.Success<IReadOnlyList<GithubRepo>>(highlightedProjects);
            }
            return  Result.Fail<IReadOnlyList<GithubRepo>>(Error.EmptyValue);
        }

    }