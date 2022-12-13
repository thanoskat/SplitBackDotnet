using System.ComponentModel.DataAnnotations;
namespace SplitBackDotnet.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class User
{
  [BsonId]
  [BsonRepresentation(BsonType.ObjectId)]
  public string Id { get; set; }

  [MaxLength(200)]
  [Required]
  public string Nickname { get; set; } = String.Empty;

  [MaxLength(200)]
  [Required]
  public string Email { get; set; } = String.Empty;

  public ICollection<Group> Groups { get; set; } = new List<Group>();

  public DateTime Date { get; set; } = DateTime.UtcNow;

}
