using Microsoft.AspNetCore.Components;

namespace MyProfile.Services;
public class NavigationService
{
    private readonly NavigationManager _navigationManager;
    private readonly string _baseUrl;

    public NavigationService(NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
        _baseUrl = _navigationManager.BaseUri.Contains("github.io") ? "/my-profile" : "";
    }

    public void GoBack()
    {
        NavigateTo("/");
    }

    public void NavigateToProjectDetails(int id)
    {
        NavigateTo($"/project-details/{id}");
    }

    public void NavigateTo(string path)
    {
        var fullPath = GetPath(path);
        _navigationManager.NavigateTo(fullPath);
    }

    public string GetPath(string relativePath)
    {

        relativePath = relativePath.TrimStart('/');
        return $"{_baseUrl}/{relativePath}";
    }
}