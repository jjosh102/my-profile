namespace MyProfile.Models;
public class CommitDisplay
{
  public string AuthorName { get; set; } = string.Empty;
  public string AuthorAvatarUrl { get; set; } = string.Empty;
  public DateTime CommitDate { get; set; }
  public string Message { get; set; } = string.Empty;
  public string CommitUrl { get; set; } = string.Empty;
}