using Microsoft.AspNetCore.Components;
using MyProfile.Models;
using MyProfile.Services;
using MyProfile.Services.Github;

namespace MyProfile.Components;
public partial class Projects : ComponentBase
{
  private readonly IGithubHttpClient _githubClient;

  private readonly NavigationService _naviagtionService;
  private bool _isApiError;
  private IReadOnlyList<GithubRepo>? _githubProjects = [];
  private static readonly Dictionary<string, string> LanguageColors = new(StringComparer.OrdinalIgnoreCase)
    {
        { "CSS", "#563d7c" },
        { "HTML", "#e34c26" },
        { "JavaScript", "#f1e05a" },
        { "Python", "#3572A5" },
        { "C#", "#178600" },
        { "Java", "#b07219" },
        { "Ruby", "#701516" },
        { "PHP", "#4f5b93" },
        { "Go", "#00ADD8" },
        { "C", "#555555" },
        { "C++", "#f34b7d" },
        { "TypeScript", "#2b7489" },
        { "Swift", "#ffac45" },
        { "Kotlin", "#7f52ff" },
        { "Rust", "#000000" },
        { "Dart", "#00B4AB" },
        { "Scala", "#c22d40" },
        { "Shell", "#89e051" },
        { "PowerShell", "#012456" },
        { "R", "#276dc3" },
        { "Lua", "#000080" },
        { "Objective-C", "#438eff" },
        { "Elixir", "#6e4a7e" },
        { "Perl", "#0298c3" },
        { "Groovy", "#2f6a8f" },
        { "Vue", "#42b883" }
    };

  public Projects(IGithubHttpClient githubClient, NavigationService navigationService)
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

  private static string GetTimeAgo(DateTime lastUpdate)
  {
    var timeDifference = DateTime.UtcNow - lastUpdate;

    const int secondsInMinute = 60;
    const int minutesInHour = 60;
    const int hoursInDay = 24;
    const int daysInMonth = 30;
    const int daysInYear = 365;

    double totalSeconds = timeDifference.TotalSeconds;
    double totalMinutes = timeDifference.TotalMinutes;
    double totalHours = timeDifference.TotalHours;
    double totalDays = timeDifference.TotalDays;

    if (totalSeconds < secondsInMinute)
    {
      return $"{(int)totalSeconds} second{(totalSeconds > 1 ? "s" : "")} ago";
    }
    else if (totalMinutes < minutesInHour)
    {
      return $"{(int)totalMinutes} minute{(totalMinutes > 1 ? "s" : "")} ago";
    }
    else if (totalHours < hoursInDay)
    {
      return $"{(int)totalHours} hour{(totalHours > 1 ? "s" : "")} ago";
    }
    else if (totalDays < 2)
    {
      return "Yesterday";
    }
    else if (totalDays < daysInMonth)
    {
      return $"{(int)totalDays} day{(totalDays > 1 ? "s" : "")} ago";
    }
    else if (totalDays < daysInYear)
    {
      return $"on {lastUpdate:MMM d}";
    }
    else
    {
      return $"on {lastUpdate:MMM d, yyyy}";
    }
  }

  private string GetLanguageBackgroundColor(string? language)
  {
    if (string.IsNullOrWhiteSpace(language))
    {
      return "#cccccc";
    }
    return LanguageColors.TryGetValue(language, out var color) ? color : "#cccccc";
  }

  private void NavigateToProjectDetails(int id) => _naviagtionService.NavigateToProjectDetails(id);
  
}


