using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace SplitBackDotnet.Models;

public class Invitation
{
  //[BsonId]
  //[BsonRepresentation(BsonType.ObjectId)]
  public ObjectId Id { get; set; }
  public ObjectId Inviter { get; set; }

  [MaxLength(10)]
  public string Code { get; set; } = null!;
  public ObjectId GroupId { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}