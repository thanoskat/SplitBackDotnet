using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
namespace SplitBackDotnet.Models;

public class Expense
{
  public ObjectId Id { get; set; }
  [MaxLength(100)]
  public string Description { get; set; } = null!;
  [Required]
  public decimal Amount { get; set; }
  public ICollection<ExpenseSpender> ExpenseSpenders { get; set; } = new List<ExpenseSpender>();
  [Required]
  public ICollection<ExpenseParticipant> ExpenseParticipants { get; set; } = new List<ExpenseParticipant>();
  public Label? Label { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public string IsoCode { get; set; } = null!;
}