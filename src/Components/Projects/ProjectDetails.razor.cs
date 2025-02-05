

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MyProfile.Models;
using MyProfile.Services;
using MyProfile.Services.Github;

namespace MyProfile.Components.Projects;

public partial class ProjectDetails : ComponentBase
{
  private readonly IGithubHttpClient _githubClient;
  private readonly NavigationService _navigationService;
  private GithubRepo? _repoData;

  public ProjectDetails(IGithubHttpClient githubHttpClient, NavigationService navigationService)
  {
    _githubClient = githubHttpClient;
    _navigationService = navigationService;
  }

  [Parameter]
  public int Id { get; set; }

  protected override async Task OnParametersSetAsync()
  {
    if (await _githubClient.GetReposToBeShownAsync() is { } gitHubRepos)
    {
      _repoData = gitHubRepos.Value?.FirstOrDefault(x => x.Id == Id);
    }
  }

  private void GoBack() => _navigationService.GoBack();
}