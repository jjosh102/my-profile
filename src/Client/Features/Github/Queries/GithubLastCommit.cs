using System.Net.Http.Json;
using MediatR;
using MyProfile.Features.Github.Constants;
using MyProfile.Features.Github.Services;
using MyProfile.Shared;
using MyProfile.Shared.DTO;

namespace MyProfile.Features.Github.Queries;

public record GetLastCommit() : IRequest<Result<GithubLastCommit>>;

public class GetLastCommitHandler : IRequestHandler<GetLastCommit, Result<GithubLastCommit>>
{
    private readonly IGithubHttpClient _githubHttpClient;

    public GetLastCommitHandler(IGithubHttpClient githubHttpClient)
    {
        _githubHttpClient = githubHttpClient;
    }

    public async Task<Result<GithubLastCommit>> Handle(GetLastCommit request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _githubHttpClient.GetRepoLastCommit("MyProfile");
            return Result.Success<GithubLastCommit>(result);
        }
        catch (Exception ex)
        {
            return Result.Fail<GithubLastCommit>(ex.Message);
        }
    }
}