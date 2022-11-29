namespace SplitBackDotnet.Models;
using System.ComponentModel.DataAnnotations;

public class Expense
{
  public int ExpenseId { get; set; }

  [MaxLength(200)]
  public string Description { get; set; } = null!;
  [Required]
  public decimal Amount { get; set; }
  public Label? Label { get; set; }
  [Required]
  public ICollection<ExpenseSpender> ExpenseSpenders { get; set; } = new List<ExpenseSpender>();
  [Required]
  public ICollection<ExpenseParticipant> ExpenseParticipants { get; set; } = new List<ExpenseParticipant>();
  public Currency Currency { get; set; } = null!;
  public short IsoCode { get; set; }
}
