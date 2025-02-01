using Microsoft.AspNetCore.Components;
using MyProfile.Models;
using MyProfile.Services;
using MyProfile.Services.Github;

namespace MyProfile.Components.Projects;
public partial class DisplayProjects : ComponentBase
{
  private readonly IGithubHttpClient _githubClient;

  private readonly NavigationService _naviagtionService;
  private bool _isApiError;
  private IReadOnlyList<GithubRepo>? _githubProjects = [];
  
  public DisplayProjects(IGithubHttpClient githubClient, NavigationService navigationService)
  {
    _githubClient = githubClient;
    _naviagtionService = navigationService;
  }

  protected override async Task OnInitializedAsync()
  {
    if (await _githubClient.GetReposToBeShown() is { } gitHubRepos)
    {
      _githubProjects = gitHubRepos.Value;
      _isApiError = gitHubRepos.IsFailure;
    }
  }

  private void NavigateToProjectDetails(int id) => _naviagtionService.NavigateToProjectDetails(id);
  
}


