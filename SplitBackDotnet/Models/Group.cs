using System.ComponentModel.DataAnnotations;
namespace SplitBackDotnet.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Group
{
  [BsonRepresentation(BsonType.ObjectId)]
  public ObjectId Id { get; set; }

  [MaxLength(50)]
  [Required]
  public string Title { get; set; } = String.Empty;
  [BsonRepresentation(BsonType.ObjectId)]
  public ObjectId CreatorId { get; set; }
  [Required]
  [BsonRepresentation(BsonType.ObjectId)]
  public ObjectId[] Members { get; set; } = Array.Empty<ObjectId>();
  public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
  public ICollection<Transfer> Transfers { get; set; } = new List<Transfer>();
  public ICollection<Label> GroupLabels { get; set; } = new List<Label>();
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}