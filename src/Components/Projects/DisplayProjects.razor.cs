using Microsoft.AspNetCore.Components;
using MyProfile.Models;
using MyProfile.Services;
using MyProfile.Services.Github;

namespace MyProfile.Components.Projects;
public partial class DisplayProjects : ComponentBase
{
  private readonly IGithubHttpClient _githubClient;
  private readonly NavigationService _navigationService;
  private IReadOnlyList<GithubRepo>? _githubProjects = [];
  
  public DisplayProjects(IGithubHttpClient githubClient, NavigationService navigationService)
  {
    _githubClient = githubClient;
    _navigationService = navigationService;
  }

  protected override async Task OnInitializedAsync()
  {
    if (await _githubClient.GetReposToBeShown() is { } gitHubRepos)
    {
      _githubProjects = gitHubRepos.Value;
    }
  }

  private void NavigateToProjectDetails(int id) => _navigationService.NavigateToProjectDetails(id);
  
}


