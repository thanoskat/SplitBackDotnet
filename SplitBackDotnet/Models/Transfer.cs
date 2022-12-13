namespace SplitBackDotnet.Models;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Transfer
{
  [BsonId]
  [BsonRepresentation(BsonType.ObjectId)]
  public string Id { get; set; } = null!;
  [MaxLength(80)]
  public string? Description { get; set; }
  public decimal Amount { get; set; }
  public string IsoCode { get; set; } = null!;
  [BsonRepresentation(BsonType.ObjectId)]
  public string SenderId { get; set; }
  [BsonRepresentation(BsonType.ObjectId)]
  public string ReceiverId { get; set; }
  public DateTime CreatedAt { get; set; }= DateTime.UtcNow;
}