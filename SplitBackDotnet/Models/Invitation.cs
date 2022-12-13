using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace SplitBackDotnet.Models;

public class Invitation
{
  [BsonId]
  [BsonRepresentation(BsonType.ObjectId)]
  public string Id { get; set; } = null!;
  [BsonRepresentation(BsonType.ObjectId)]
  public string Inviter { get; set; } = null!;

  [MaxLength(10)]
  public string Code { get; set; } = null!;
  [BsonRepresentation(BsonType.ObjectId)]
  public string GroupId { get; set; } = null!;
  public DateTime CreatedAt { get; set; }

}