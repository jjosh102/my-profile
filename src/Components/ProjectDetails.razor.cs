

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MyProfile.Models;
using MyProfile.Services.Github;

namespace MyProfile.Components;

public partial class ProjectDetails : ComponentBase
{
  private readonly IGithubHttpClient _githubClient;
  private readonly IJSRuntime _jsRuntime;

  private readonly NavigationManager _navigationManager;

  private GithubRepo? _repoData;

  public ProjectDetails(IGithubHttpClient githubHttpClient, IJSRuntime jsRuntime, NavigationManager navigationManager)
  {
    _githubClient = githubHttpClient;
    _jsRuntime = jsRuntime;
    _navigationManager = navigationManager;
  }

  [Parameter]
  public int Id { get; set; }

  protected override async Task OnInitializedAsync()
  {
    if (await _githubClient.GetReposToBeShown() is { } gitHubRepos)
    {
      _repoData = gitHubRepos.Value?.FirstOrDefault(x => x.Id == Id);
    }
  }

  protected override async Task OnAfterRenderAsync(bool firstRender)
  {
    if (_repoData != null)
    {
      await RenderChart();
    }
  }

  private async Task RenderChart()
  {
    var ctx = await _jsRuntime.InvokeAsync<IJSObjectReference>("getChartContext", "repoStatsChart");
    await _jsRuntime.InvokeVoidAsync("renderRepoStatsChart", ctx, _repoData!.StargazersCount, _repoData.ForksCount, _repoData.WatchersCount);
  }

  private void GoBack()
  {
    _navigationManager.NavigateTo("/");
  }
}