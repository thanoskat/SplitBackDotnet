namespace SplitBackDotnet.Models;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Expense
{
  [BsonId]
  [BsonRepresentation(BsonType.ObjectId)]
  public string Id { get; set; } = null!;
  [MaxLength(100)]
  public string Description { get; set; } = null!;
  [Required]
  public decimal Amount { get; set; }
  public ICollection<ExpenseSpender> ExpenseSpenders { get; set; } = new List<ExpenseSpender>();
  [Required]
  public ICollection<ExpenseParticipant> ExpenseParticipants { get; set; } = new List<ExpenseParticipant>();
  public Label? Label { get; set; }
  public DateTime CreatedAt { get; set; }
  public string IsoCode { get; set; }
  
}
