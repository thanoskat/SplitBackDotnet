namespace SplitBackDotnet.Models;
using MongoDB.Bson;

public class Session
{
  public ObjectId Id { get; set; }
  public string RefreshToken { get; set; } = String.Empty;
  public ObjectId UserId { get; set; }
  public string? Unique { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}