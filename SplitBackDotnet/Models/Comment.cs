using MongoDB.Bson;
namespace SplitBackDotnet.Models;

public class Comment
{
  public ObjectId Id { get; set; }
  public ObjectId CommentorId { get; set; }
  public string? Text { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}