namespace SplitBackDotnet.Models;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Session
{
  [BsonId]
  [BsonRepresentation(BsonType.ObjectId)]
  public string Id { get; set; }
  [Required]
  public string RefreshToken { get; set; } = String.Empty;
  [Required]
  [BsonRepresentation(BsonType.ObjectId)]
  public string UserId { get; set; }
  public string? Unique { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}