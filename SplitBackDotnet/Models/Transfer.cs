namespace SplitBackDotnet.Models;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Transfer
{
  public ObjectId Id { get; set; }
  [MaxLength(80)]
  public string? Description { get; set; }
  public decimal Amount { get; set; }
  public string IsoCode { get; set; } = null!;
  public ObjectId SenderId { get; set; }
  public ObjectId ReceiverId { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}