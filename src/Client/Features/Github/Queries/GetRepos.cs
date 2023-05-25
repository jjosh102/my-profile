using MediatR;
using MyProfile.Features.Github.Services;
using MyProfile.Shared;
using MyProfile.Shared.DTO;

namespace MyProfile.Features.Github.Queries;
public record GetHighlightedRepos : IRequest<Result<IReadOnlyList<GithubRepo>>>;

public class GetReposHandler : IRequestHandler<GetHighlightedRepos, Result<IReadOnlyList<GithubRepo>>>
{
    private readonly IGithubHttpClient _githubHttpClient;

    public GetReposHandler(IGithubHttpClient githubHttpClient)
    {
        _githubHttpClient = githubHttpClient;
    }

    public async Task<Result<IReadOnlyList<GithubRepo>>> Handle(GetHighlightedRepos request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _githubHttpClient.GetRepos();
            var highlightedProjects = result.Where(t => t.Topics.Contains("show")).ToList();
            return Result.Success<IReadOnlyList<GithubRepo>>(highlightedProjects);
        }
        catch (Exception ex)
        {
            return Result.Fail<IReadOnlyList<GithubRepo>>(ex.Message);
        }
    }
}